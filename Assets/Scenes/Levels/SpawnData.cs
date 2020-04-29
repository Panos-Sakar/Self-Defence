

namespace Scenes.Levels
{
    [System.Serializable]
    public class SpawnData
    {
        public string[] enemyIDs =
        {
            "Default",
            "SmallBall",
            "BigBall"
        };
        
        public SpawnPoint[] points;
        public EnemyPool[] pools;
    }
    
    [System.Serializable]
    public class SpawnPoint
    {
        public string pointName;
        public float spawnRate;
        public Position position;

        public string[] pattern;
    }
    
    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
    [System.Serializable]
    public class EnemyPool
    {
        public string enemyName;
        public int size;
        public bool canGrow;
    }
}
