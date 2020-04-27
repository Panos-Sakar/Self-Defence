using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class PlayerLogicScript : MonoBehaviour
{
    public static PlayerLogicScript Instance { get; private  set; }
    
    [Header("References")]
    [SerializeField] private PlayerInput _playerInputVar = null;
    [SerializeField] private SpawnProjectiles projectileSystem = null;
    [SerializeField] private GameObject headInnerTransform = null;
    [SerializeField] private GameObject headGridTransform = null;
    [SerializeField] private GameObject uberKillObject = null;
    
    private PlayerInputActions _inputActionsVar = null;
    
    [Header("Player Attributes")]
    [SerializeField] private float headRotationSpeed = 50f;
    [SerializeField] private float maxLife = 10;
    private float life = 0;
    
    [SerializeField] private float maxStamina = 10;
    private float stamina = 0;
    
    [ExecuteInEditMode]
    public int money = 0;
    
    [SerializeField] private float staminaRegenPeriod;
    
    private Transform _myTransform;
    private Slider _healthRef;
    private Slider _staminaRef;
    private TextMeshProUGUI _moneyRef;
    private TextMeshProUGUI _titleRef;
    private const string Padding = "    ";

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
        _myTransform = transform;
        InitializeInputSystem();
    }
    
    void Start()
    {
        InvokeRepeating("IncreaseStamina",0,staminaRegenPeriod );
        
        GetSliders();
        GetTextFields();

        InitializeValues();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotatePlayerHead();
        
        if (life <=0)
        {
            KillPlayer();
        }
        else
        {
            UpdatePlayerStats();
        }

        if (money > 10 && !PlayerUpgrades.Instance.explodeOnImpact)
        {
            UserInterfaceHandler.Instance.ActivateButton("ImpactExplosion");
        }
        
        if (money > 20 && !PlayerUpgrades.Instance.ultimate)
        {
            UserInterfaceHandler.Instance.ActivateButton("Ultimate");
        }
    }
    
    private void RotatePlayerHead()
    {
        headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        headInnerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
    }

    private void UpdatePlayerStats()
    {
        _healthRef.value = life;
        _staminaRef.value = stamina;
        _moneyRef.text = Padding + money.ToString();
        #if UNITY_EDITOR
        UserInterfaceHandler.Instance.PrintToDebug(2,"Life: " + life + " Stamina: " + stamina);
        #endif
    }

    private void KillPlayer()
    {
        life = 0;
        stamina = 0;
        money = 0;

        _healthRef.value = life;
        _staminaRef.value = stamina;
        _titleRef.text = Padding + "CPhage is DEAD :(";
        _moneyRef.text = Padding + money.ToString();
    }

    private void GetSliders()
    {
        Slider[] allSliders = GameObject.FindObjectsOfType<Slider>();
        
        foreach (Slider slider in allSliders)
        {
            if (slider.name == "Health")
            {
                _healthRef = slider;
            }
            else if (slider.name == "Stamina")
            {
                _staminaRef = slider;

            }
        }
    }

    private void GetTextFields()
    {
        TextMeshProUGUI[] allTextFields = GameObject.FindObjectsOfType<TextMeshProUGUI>();;
        foreach (TextMeshProUGUI textField in allTextFields)
        {
            if (textField.name == "Title")
            {
                _titleRef = textField;
            }
            else if (textField.name == "MoneyAmount")
            {
                _moneyRef = textField;
            }

        }
    }

    private void InitializeValues()
    {
        life = maxLife;
        // Life Init -----------------------------------------------------

        _healthRef.maxValue = maxLife;
        _healthRef.value = life;
        
        // Stamina Init -----------------------------------------------------
        _staminaRef.maxValue = maxStamina;
        _staminaRef.value = stamina;
        
        // Title Init -----------------------------------------------------
        _titleRef.text = Padding + "Defend the CPhage!";

        // Money Init -----------------------------------------------------
        _moneyRef.text = Padding + money.ToString();
    }

    private  void IncreaseStamina()
    {
        stamina += 1;
        
        if (stamina > maxStamina) stamina = maxStamina; 
    }

    public void GiveMoney(int amount)
    {
        money += amount;
    }
    
    public void TakeMoney(int amount)
    {
        money -= amount;
    }

    public void GiveLife(int amount)
    {
        
        life += amount;
    }
    
    public void IncreaseLife(int amount)
    {
        maxLife += amount;
        life += amount;
    }
    
    public void GiveStamina(int amount)
    {
        
        stamina += amount;
    }
    
    public void IncreaseStamina(int amount)
    {
        maxStamina += amount;
        stamina += amount;
    }
    private void OnEnable()
    {
        _inputActionsVar.Enable();
        InputUser.onChange += OnInputDeviceChange;
    }

    private void OnDisable()
    {
        _inputActionsVar.Disable();
        InputUser.onChange -= OnInputDeviceChange;
    }

    private void FireProjectile(InputAction.CallbackContext context)
    {
        if (stamina > 0)
        {
            projectileSystem.SpawnFireEffect();
            stamina -= 1;
        }
    }
    
    private void UberKill(InputAction.CallbackContext context)
    {
        if (stamina >= 5 && PlayerUpgrades.Instance.ultimate)
        {
            Vector3[] position = new []
            {
                _myTransform.position + new Vector3(0,2,0), 
                _myTransform.forward,
                _myTransform.right,
                -_myTransform.forward,
                -_myTransform.right
            };

            for (int i = 1; i < 5; i++)
            {
                Instantiate(uberKillObject, position[0], Quaternion.LookRotation (position[i]));
            }
            
            stamina -= 5;
        }

    }

    private void InitializeInputSystem()
    {
        _inputActionsVar = new PlayerInputActions();
        
        _inputActionsVar.PlayerControls.Fire.performed += FireProjectile;
        _inputActionsVar.PlayerControls.ContextMenu.performed += UberKill;
    }
    
    private void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {
        if (change == InputUserChange.ControlSchemeChanged) {UserInterfaceHandler.Instance.ToggleInputIcon(_playerInputVar.currentControlScheme);}
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            float damage = headRotationSpeed;
            
            life -= damage;
            
            if (life < 0) life = 0;
        }
    }
}