using System.Collections;
using SelfDef.Interfaces;
using SelfDef.Systems.Loading;
using SelfDef.Variables;
using Unity.Mathematics;
using UnityEngine;


namespace SelfDef.Change_Cubes
{
    public class UpgradeCube : MonoBehaviour , ICanChangeSettings, IGiveUpgrade
    {
#pragma warning disable CS0649
        
        public Vector3 StartPosition { get; set; }
        public bool StopAnimation { get; set; }


        public string TipText
        {
            get => tipText;
            set => tipText = value;
        }

        public int Cost
        {
            get => abilityCost;
            set => abilityCost = value;
        }

        [Header("Cube parameters")] 
        [SerializeField] 
        private PlayerVariables.PlayerAbilities abilityType;
        [SerializeField] 
        private int abilityCost;
        [SerializeField]
        private string tipText;
        [SerializeField] 
        private Sprite icon;

        [Header("References")]
        [SerializeField]
        private PlayerVariables playerVariable;
        [SerializeField]
        private Animator animator;

        [SerializeField] 
        private GameObject mother;

        public PlayerVariables PlayerVariable
        {
            get => playerVariable;
            set => playerVariable = value;
        }

        

        private bool _animationLock;
        private bool _abilityGiven;
        private LoadingHandler _loadingHandler;
        

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Hide = Animator.StringToHash("Hide");

#pragma warning restore CS0649
        private void Start()
        {
            
            
            mother.transform.parent = null;
            DontDestroyOnLoad(mother);
            
            if(playerVariable.playerAbilities[abilityType]) Destroy(mother);
            
            _loadingHandler = LoadingHandler.Instance;
            
            StartPosition = new Vector3(-3,40,-5);
            
            StopAnimation = false;
            _animationLock = false;
            _abilityGiven = false;
            
            _loadingHandler.playerFinishedLevel.AddListener(ResetPosition);
            _loadingHandler.levelLoadingStarted.AddListener(Lock);
            
            gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (StopAnimation)
            {
                StopAnimation = false;
                _animationLock = false;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var colliderSettings = other.GetComponent<IActivateSettings>();

            if (colliderSettings == null) return;

            if (!colliderSettings.GiveUpgrade) return;
            
            if(_animationLock) return;
            
            StartCoroutine(Explode(2f));
        }

        public (bool, string) GetSecondaryText()
        {
            return (false, "");
        }

        public IEnumerator Explode(float delay)
        {
            if (abilityCost > playerVariable.money) yield break;
            _animationLock = true;
            
            playerVariable.money -= abilityCost;
            playerVariable.playerAbilities[abilityType] = true;
            _abilityGiven = true;
            animator.SetTrigger(Hit);
            
            yield return new WaitForSeconds(delay);
            StopAnimation = true;
            Kill();
        }

        public void ResetPosition()
        {
            if (_abilityGiven) return;
            
            var obj = gameObject;
            
            obj.transform.position = StartPosition;
            obj.transform.localScale = Vector3.one/2;
            obj.transform.localRotation = quaternion.identity;
            gameObject.SetActive(true);
        }

        public void Lock()
        {
            if (!gameObject.activeInHierarchy) return;
            
            StartCoroutine(Disappear(0.9f));
        }
        
        private IEnumerator Disappear(float delay)
        {
            animator.SetTrigger(Hide);
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        public void Kill()
        {
            _loadingHandler.playerFinishedLevel.RemoveListener(ResetPosition);
            _loadingHandler.levelLoadingStarted.RemoveListener(Lock);
            gameObject.SetActive(false);
        }

        public Sprite GetIcon()
        {
            return icon;
        }
    }
}
