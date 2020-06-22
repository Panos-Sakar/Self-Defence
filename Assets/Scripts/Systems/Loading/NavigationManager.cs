using UnityEngine;

namespace SelfDef.Systems.Loading
{
    public class NavigationManager : MonoBehaviour
    {
        public static NavigationManager Instance { get; private  set; }

        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
        }

        public void ChangeNavMesh(int levelIndex)
        {
            
        }
    }
}
