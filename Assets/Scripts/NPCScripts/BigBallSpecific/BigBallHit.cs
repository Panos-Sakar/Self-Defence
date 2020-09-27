using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace SelfDef.NPCScripts.BigBallSpecific
{
    public class BigBallHit : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private Rigidbody body;

        private Coroutine _animation;
        private bool _animationIsRunning;
        
        private void Awake()
        {
            _animationIsRunning = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                if (!_animationIsRunning && gameObject.activeInHierarchy) _animation = StartCoroutine(AddForce());
            }
        }
        
#pragma warning restore CS0649
        private  IEnumerator AddForce()
        {
            _animationIsRunning = true;
            agent.enabled = false;
            
            body.isKinematic = false;
            body.AddForce(Vector3.back*3,ForceMode.Impulse);

            yield return new WaitForSeconds(1f);

            ResetAfterAnimation();
            
            yield return null;
        }
        
        private void ResetAfterAnimation()
        {
            if (agent.isActiveAndEnabled)
            {
                agent.enabled = true;
                agent.isStopped = true;
            }

            body.isKinematic = true;
            _animationIsRunning = false;
        }
        
        private void OnDisable()
        {
            if (_animationIsRunning)
            {
                StopCoroutine(_animation);
                ResetAfterAnimation();
            }

        }
    }
}
