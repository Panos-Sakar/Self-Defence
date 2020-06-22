using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Tools
{
    [RequireComponent(typeof(Initializer))]
    public class EditSpawnPoints : MonoBehaviour
    {
        [HideInInspector]
        public LevelSpawnData data;

        public Mesh positionItemMesh;
        public Material positionItemMaterial;

        [Range(-2,2)]
        public float labelVerticalOffset = 1;
        public GUIStyle labelStyle = new GUIStyle();
        
        public bool createPositionalObjects;
        public bool updatePositionalObjects;
        public bool deletePositionalObjects;
        
        private void OnValidate()
        {
            data = GetComponent<Initializer>().GetLevelData();
        }

        public void UpdateData()
        {
            data = GetComponent<Initializer>().GetLevelData();
        }
    }
}
