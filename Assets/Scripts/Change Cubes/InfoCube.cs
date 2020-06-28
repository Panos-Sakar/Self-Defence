using System.Collections;
using SelfDef.Interfaces;
using SelfDef.Systems.Loading;
using Unity.Mathematics;
using UnityEngine;


namespace SelfDef.Change_Cubes
{
    public class InfoCube : MonoBehaviour, ICanChangeSettings
    {
#pragma warning disable CS0649
        
        public Vector3 StartPosition { get; set; }
        public bool StopAnimation { get; set; }
        
        [Header("Cube parameters")]
        [SerializeField]
        private string tipText;
        [SerializeField] 
        private Sprite icon;

        public string TipText
        {
            get => tipText;
            set => tipText = value;
        }
        
        [Header("References")]
        [SerializeField] 
        private GameObject mother;
        [SerializeField]
        private Animator animator;

        private bool _animationLock;
        private LoadingHandler _loadingHandler;
        

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Hide = Animator.StringToHash("Hide");

#pragma warning restore CS0649
        private void Awake()
        {
            mother.transform.parent = null;
            DontDestroyOnLoad(mother);
            _loadingHandler = LoadingHandler.Instance;
            
            StartPosition = new Vector3(-3,40,-5);
            
            StopAnimation = false;
            _animationLock = false;
            
            _loadingHandler.levelLoadingStarted.AddListener(Lock);
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

            if(_animationLock) return;
            
            StartCoroutine(Explode(1f));
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public IEnumerator Explode(float delay)
        {
            _animationLock = true;
            
            animator.SetTrigger(Hit);
            
            yield return new WaitForSeconds(delay);
            
            StopAnimation = true;
            
            Kill();
        }

        public void ResetPosition()
        {
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
            
            Kill();
        }

        public void Kill()
        {
            _loadingHandler.levelLoadingStarted.RemoveListener(Lock);
            Destroy(mother);
        }
    }
}
