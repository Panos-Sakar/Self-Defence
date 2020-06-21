namespace SelfDef.Systems.SpawnSystem
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
        
        public SpawnPointData[] points;
        public EnemyPoolData[] pools;
    }
    
    [System.Serializable]
    public class SpawnPointData
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
    public class EnemyPoolData
    {
        public string enemyName;
        public int size;
        public bool canGrow;
    }
}