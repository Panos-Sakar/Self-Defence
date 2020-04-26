using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerLogicScript : MonoBehaviour
{
    private Transform _myTransform;
    [SerializeField] private float headRotationSpeed = 50f;
    [SerializeField] private SpawnProjectiles projectileSystem = null;
    [SerializeField] private GameObject userInterfaceCanvas;
    [SerializeField] private GameObject uberKillObject = null;
    public GameObject headInnerTransform = null;
    public GameObject headGridTransform = null;
    
    private Slider _healthRef;
    private Slider _staminaRef;
    private TextMeshProUGUI _moneyRef;
    private TextMeshProUGUI _titleRef;
    private string _padding = "    ";

    private PlayerInputActions _inputActionsVar;


    [SerializeField] public float maxLife = 10;
    private float life = 0;
    
    [SerializeField] public float maxStamina = 10;
    private float stamina = 0;
    
    [SerializeField] public int maxMoney = 1000;
    private int money = 0;
    
    [SerializeField] public float staminaRegenPeriod;
    
    // Start is called before the first frame update
    void Awake()
    {
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
        headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        headInnerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        

        if (life <=0)
        {
            life = 0;
            stamina = 0;
            money = 0;
            
            _healthRef.value = life;
            _staminaRef.value = stamina;
            _titleRef.text = _padding + "CPhage is DEAD :(";
            _moneyRef.text = _padding + money.ToString();
        }
        else
        {
            _healthRef.value = life;
            _staminaRef.value = stamina;
            _moneyRef.text = _padding + money.ToString();
        }
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
        _titleRef.text = _padding + "Defend the CPhage!";

        // Money Init -----------------------------------------------------
        _moneyRef.text = _padding + money.ToString();
    }

    private  void IncreaseStamina()
    {
        stamina += 1;
        
        if (stamina > maxStamina) stamina = maxStamina; 
    }

    public void giveMoney(int amount)
    {
        money += amount;
    }

    private void OnEnable()
    {
        _inputActionsVar.Enable();
    }

    private void OnDisable()
    {
        _inputActionsVar.Disable();
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
        if (stamina >= 5)
        {
            Vector3 pos = _myTransform.position + new Vector3(0,2,0);
            Vector3 forward = _myTransform.forward;
            Vector3 right = _myTransform.right;
            Vector3 up = _myTransform.up;
        
            Instantiate(uberKillObject, pos, Quaternion.LookRotation (forward));
            Instantiate(uberKillObject, pos, Quaternion.LookRotation (-forward));
            Instantiate(uberKillObject, pos, Quaternion.LookRotation (right));
            Instantiate(uberKillObject, pos, Quaternion.LookRotation (-right));
            
            stamina -= 5;
        }

    }

    private void InitializeInputSystem()
    {
        _inputActionsVar = new PlayerInputActions();
        _inputActionsVar.PlayerControls.Fire.performed += FireProjectile;
        _inputActionsVar.PlayerControls.ContextMenu.performed += UberKill;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            float damage = headRotationSpeed;
            
            life -= damage;
            
            if (life < 0) life = 0;
        }
    }
}