using SelfDef.PlayerScripts;
using UnityEngine;
using UnityEngine.AI;

namespace SelfDef.NPCScripts
{
    public class TetrahedronMovement : MonoBehaviour
    {
        private Transform _myTransform;
        
        private NavMeshAgent _agent;
        private Animator _animator;
        private static readonly int Fire = Animator.StringToHash("StartMoveAnimation");


        private Quaternion _startRotation;
        private Vector3 _startScale;
        
        // Start is called before the first frame update
        private void Awake()
        {
            _myTransform = gameObject.transform;
            
            _startRotation = _myTransform.rotation;
            _startScale = _myTransform.localScale;
            
            _agent = gameObject.GetComponentInParent<NavMeshAgent>();
            _animator =  gameObject.GetComponentInParent<Animator>();
            var player = gameObject.GetComponentInParent<EnemyLogic>()?.player;
            if (player != null) player.GetComponent<PlayerLogicScript>().PlayerFiredProjectile += MakeStep;
        }

        private void MakeStep()
        {
            if(gameObject.activeInHierarchy) _agent.isStopped = false;
            _animator.SetBool(Fire, true);
        }

        private void StepEnded()
        {
            _animator.SetBool(Fire, false);
            if(gameObject.activeInHierarchy) _agent.isStopped = true;
            
        }

        private void OnEnable()
        {
            _agent.isStopped = true;
            _animator.Play("Tetrahedron_Idle", -1, 0f);
            
        }

        private void OnDisable()
        {
            _myTransform.rotation = _startRotation;
            _myTransform.localScale = _startScale;

        }
    }
}
