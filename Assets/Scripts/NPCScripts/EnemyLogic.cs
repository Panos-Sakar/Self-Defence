using SelfDef.PlayerScripts;
using SelfDef.Prefabs.Projectiles.Arrow.Prefab.Scripts;
using SelfDef.Systems.SpawnSystemV2;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SelfDef.NPCScripts
{
    public class EnemyLogic : MonoBehaviour
    {
#pragma warning disable CS0649
        
        public GameObject player;
        private NavMeshAgent _enemyNavMesh;
        private Transform _playerTransform;
        private Transform _myTransform;
        
        [SerializeField] private GameObject explosionParticle;
        [SerializeField] private GameObject hitParticle;

        [SerializeField] public float damageAmount;
        [SerializeField] public float life;
        [SerializeField] public float maxLife;
        [SerializeField] public int money;
        
        private Vector3 _particlePosition;
        private Quaternion _particleRotation;

        [SerializeField] public EnemyTypes enemyType = EnemyTypes.Default;

#pragma warning restore CS0649
        private void Awake()
        {
            gameObject.SetActive(false);
            
            _enemyNavMesh = GetComponent<NavMeshAgent>();
        }
        
        private void OnEnable()
        {

            if (player == null) GameObject.FindGameObjectWithTag("Player");

            _playerTransform = player.transform;
            _playerTransform.localScale = new Vector3(1,1,1);
            _enemyNavMesh.destination = _playerTransform.position;
            
        }

        public void Initialize(string newName, GameObject playerRef )
        {
            name = newName;
            player = playerRef;
            
            _myTransform = transform;
        }
        // Update is called once per frame

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerLogicScript.Instance.DamagePlayer(damageAmount);
                
                _particlePosition = _myTransform.position;
                _particleRotation = _myTransform.rotation;
                
                Instantiate(explosionParticle, _particlePosition, _particleRotation);
                gameObject.SetActive(false);  
            }

            if (other.gameObject.CompareTag("Projectile"))
            {
                DamageSelf(other.GetComponent<ProjectileProperties>().damage);
            }
        }

        private void DamageSelf(float amount)
        {
            life -= amount;

            if (!(life <= 0)) return;
            
            life = maxLife;
            
            _particlePosition = _myTransform.position;
            _particleRotation = _myTransform.rotation;

            player.GetComponent<PlayerLogicScript>().GiveMoney(money);
            Instantiate(hitParticle, _particlePosition, _particleRotation);
            gameObject.SetActive(false);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.Label(gameObject.transform.position + Vector3.up * 0.125f,$"HP: {life} / {maxLife}");
        }  
#endif

    }
}