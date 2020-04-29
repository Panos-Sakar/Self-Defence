using Systems.FireProjectile;
using Systems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerLogicScript : MonoBehaviour
    {
#pragma warning disable CS0649
        public static PlayerLogicScript Instance { get; private  set; }
        
        [Header("References")]
        [SerializeField] private PlayerInput playerInputVar;
        [SerializeField] private SpawnProjectiles projectileSystem;
        [SerializeField] private GameObject headInnerTransform;
        [SerializeField] private GameObject headGridTransform;
        [SerializeField] private GameObject uberKillObject;
    
        private PlayerInputActions _inputActionsVar;
    
        [Header("Player Attributes")]
        [SerializeField] private float headRotationSpeed = 50f;
        [SerializeField] private float maxLife = 10;
        private float _life;
    
        [SerializeField] private float maxStamina = 10;
        private float _stamina;

        public int money;
    
        [SerializeField] private float staminaRegenPeriod;
        [SerializeField] private float fireRate = 0.5f;
        private float _timeToFire;
    
        private Transform _myTransform;
        private Slider _healthRef;
        private Slider _staminaRef;
        private TextMeshProUGUI _moneyRef;
        private TextMeshProUGUI _titleRef;
        private const string Padding = "    ";
        
#pragma warning restore CS0649

        void Awake()
        {
            _timeToFire = 0;
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
        
            if (_life <=0)
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

            _timeToFire -= Time.deltaTime;
        }
    
        private void RotatePlayerHead()
        {
            headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
            headInnerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        }

        private void UpdatePlayerStats()
        {
            _healthRef.value = _life;
            _staminaRef.value = _stamina;
            _moneyRef.text = Padding + money.ToString();
#if UNITY_EDITOR
            UserInterfaceHandler.Instance.PrintToDebug(0,"Life: " + _life + " Stamina: " + _stamina);
#endif
        }

        private void KillPlayer()
        {
            _life = 0;
            _stamina = 0;
            money = 0;

            _healthRef.value = _life;
            _staminaRef.value = _stamina;
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
            TextMeshProUGUI[] allTextFields = GameObject.FindObjectsOfType<TextMeshProUGUI>();
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
            _life = maxLife;
            // Life Init -----------------------------------------------------

            _healthRef.maxValue = maxLife;
            _healthRef.value = _life;
        
            // Stamina Init -----------------------------------------------------
            _staminaRef.maxValue = maxStamina;
            _staminaRef.value = _stamina;
        
            // Title Init -----------------------------------------------------
            _titleRef.text = Padding + "Defend the CPhage!";

            // Money Init -----------------------------------------------------
            _moneyRef.text = Padding + money.ToString();
        }

        private  void IncreaseStamina()
        {
            _stamina += 1;
        
            if (_stamina > maxStamina) _stamina = maxStamina; 
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
        
            _life += amount;
        }
    
        public void IncreaseLife(int amount)
        {
            maxLife += amount;
            _life += amount;
        }
    
        public void GiveStamina(int amount)
        {
        
            _stamina += amount;
        }
    
        public void IncreaseStamina(int amount)
        {
            maxStamina += amount;
            _stamina += amount;
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
            if (PlayerCanFire())
            {
                projectileSystem.SpawnFireEffect();
                _stamina -= 1;
            }
        }

        private bool PlayerCanFire()
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && (_timeToFire < 0) && (_stamina > 0))
            {
                _timeToFire = fireRate;
                return true;
            }

            return false;
        }
        
        private void UberKill(InputAction.CallbackContext context)
        {
            if (PlayerCanFire() && PlayerUpgrades.Instance.ultimate)
            {
                Vector3 pos = _myTransform.position + new Vector3(0, 2, 0);
                Vector3 forward =_myTransform.forward;
                Vector3 right = _myTransform.right;
                
                Vector3[] position = {
                    forward,
                    right,
                    -forward,
                    -right
                };

                for (int i = 0; i < 4; i++)
                {
                    Instantiate(uberKillObject, pos, Quaternion.LookRotation (position[i]));
                }
            
                _stamina -= 5;
            }

        }

        private void InitializeInputSystem()
        {
            _inputActionsVar = new PlayerInputActions();
        
            _inputActionsVar.PlayerControls.Fire.performed += FireProjectile;
            _inputActionsVar.PlayerControls.ContextMenu.performed += UberKill;
        }
    
        private void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {
            if (change == InputUserChange.ControlSchemeChanged) {UserInterfaceHandler.Instance.ToggleInputIcon(playerInputVar.currentControlScheme);}
        }

        public void DamagePlayer(float amount)
        {
            _life -= amount;
            if (_life < 0) _life = 0;
        }
        
        
    }
}