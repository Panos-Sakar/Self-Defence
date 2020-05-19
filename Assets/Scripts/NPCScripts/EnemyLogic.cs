using PlayerScripts;
using Prefabs.Projectiles.Arrow.Prefab;
using UnityEngine;
using UnityEngine.AI;

namespace NPCScripts
{
    public class EnemyLogic : MonoBehaviour
    {
#pragma warning disable CS0649
        
        public enum EnemyTypeEnum
        {
            Default,
            SmallBall,
            BigBall
        };
        
        private GameObject _player;
        private NavMeshAgent _enemyNavMesh;
        private Transform _playerTransform;
        private Transform _myTransform;
        
        [SerializeField] private GameObject explosionParticle;
        [SerializeField] private GameObject hitParticle;

        [SerializeField] public float damageAmount;
        [SerializeField] public float life;
        [SerializeField] public float maxLife;
        
        private Vector3 _particlePosition;
        private Quaternion _particleRotation;

        [SerializeField] public EnemyTypeEnum enemyType = EnemyTypeEnum.Default;
        
#pragma warning restore CS0649
        // Start is called before the first frame update
        private void OnEnable()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _enemyNavMesh = GetComponent<NavMeshAgent>();
            _playerTransform = _player.transform;
            _myTransform = transform;
        }

        // Update is called once per frame
        private void Update()
        {
            if (gameObject.activeInHierarchy.Equals(true))
            {
                _enemyNavMesh.destination = _playerTransform.position;
            }

        }

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
                DamageEnemy(other.GetComponent<ProjectileProperties>().damage);
            }
        }

        public void DamageEnemy(float amount)
        {
            life -= amount;
            
            if (life <= 0)
            {
                life = maxLife;
                _particlePosition = _myTransform.position;
                _particleRotation = _myTransform.rotation;

                _player.GetComponent<PlayerLogicScript>().GiveMoney(1);
                Instantiate(hitParticle, _particlePosition, _particleRotation);
                gameObject.SetActive(false);
            }
        }
    }
}