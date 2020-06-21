using System;
using SelfDef.Interfaces;
using UnityEngine;

namespace SelfDef.Systems.Loading
{
    public class ChangeLevel : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField] private int levelIndex;

        private Vector3 _startPosition;
        
#pragma warning restore CS0649
        private void Awake()
        {
            _startPosition = gameObject.transform.position;
            LoadingHandler.Instance.playerFinishedLevel.AddListener(ResetPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            DontDestroyOnLoad(this.gameObject);
            
            var colliderSettings = other.GetComponent<IChangeSetting>();

            if (colliderSettings == null) return;

            if (!colliderSettings.ChangeLevel) return;
            
            StartCoroutine(LoadingHandler.Instance.StartLoadSequence(levelIndex, transform));

            levelIndex++;
        }

        private void ResetPosition()
        {
            gameObject.transform.position = _startPosition;
            gameObject.SetActive(true);
        }
    }
}
