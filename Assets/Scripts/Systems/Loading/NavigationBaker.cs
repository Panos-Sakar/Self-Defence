using UnityEngine;
using UnityEngine.AI;

namespace SelfDef.Systems.Loading
{
    public class NavigationBaker : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField]
        private NavMeshSurface surface;

        [SerializeField] private bool bakeOnAwake;
        
#pragma warning restore CS0649
        
        private void Awake()
        {
            if (bakeOnAwake)
            {
                Debug.Log($"Baking NavMesh");
                surface.BuildNavMesh();
            }

        }
    }
}
