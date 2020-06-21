using SelfDef.Interfaces;
using UnityEngine;

namespace SelfDef.Systems.Loading
{
    public class ChangeLevel : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField] private int levelIndex;
        
#pragma warning restore CS0649
        private void OnTriggerEnter(Collider other)
        {
            DontDestroyOnLoad(this.gameObject);
            
            var colliderSettings = other.GetComponent<IChangeSetting>();

            if (colliderSettings == null) return;

            if (!colliderSettings.ChangeLevel) return;
            
            StartCoroutine(LoadingHandler.Instance.StartLoadSequence(levelIndex, transform));

            levelIndex++;
        }
    }
}
