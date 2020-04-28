using UnityEngine;

namespace Systems.SpawnEnemies
{
    public class SpawnEnemyScript : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private EnemyPool enemyPool = null;
    
        private Transform _myTransform;
        private float _randomSeed;

#pragma warning restore CS0649
        // Start is called before the first frame update
        void Start()
        {
            if (enemyPool == null)
            {
                GetComponent<EnemyPool>();
            }
            _myTransform = transform;
            _randomSeed = Random.Range(1f, 5f);
            InvokeRepeating("SpawnEnemy", 1f + _randomSeed, 3f*_randomSeed);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        void SpawnEnemy()
        {
            GameObject enemy = enemyPool.GetAvailableObject();
        
            enemy.transform.position = _myTransform.position;
            enemy.SetActive(true);
        }
    }
}
