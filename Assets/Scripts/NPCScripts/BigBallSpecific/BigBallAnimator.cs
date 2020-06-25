using UnityEngine;

namespace SelfDef.NPCScripts.BigBallSpecific
{
    public class BigBallAnimator : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField]
        private Animator animator;

        private static readonly int Hit = Animator.StringToHash("Hit");
        
#pragma warning restore CS0649
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                animator.SetTrigger(Hit);
            }
        }
    }
}
