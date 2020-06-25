using JetBrains.Annotations;
using SelfDef.PlayerScripts;
using UnityEngine;
using UnityEngine.AI;

namespace SelfDef.NPCScripts
{
    public class TetrahedronMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private GameObject _player;

        private static readonly int Fire = Animator.StringToHash("StartMoveAnimation");
        private static readonly int Start = Animator.StringToHash("Start");


        // Start is called before the first frame update
        private void Awake()
        {
            _agent = gameObject.GetComponentInParent<NavMeshAgent>();
            _animator =  gameObject.GetComponentInParent<Animator>();
            _player = gameObject.GetComponentInParent<EnemyLogic>()?.player;
            
            if (_player != null) _player.GetComponent<PlayerLogicScript>().PlayerFiredProjectile += MakeStep;
        }

        private void MakeStep()
        {
            if(!gameObject.activeInHierarchy) return;
            
            _animator.SetBool(Fire, true);
            _agent.isStopped = false;
        }

        [UsedImplicitly]
        private void StepEnded()
        {
            if(!gameObject.activeInHierarchy) return;
            
            _animator.SetBool(Fire, false);
            _agent.isStopped = true;
            
        }

        private void OnEnable()
        {
            _animator.SetTrigger(Start);
            _agent.isStopped = true;

        }

        private void OnDestroy()
        {
            if (_player != null) _player.GetComponent<PlayerLogicScript>().PlayerFiredProjectile -= MakeStep;
        }
    }
}
