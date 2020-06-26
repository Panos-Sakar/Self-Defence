using UnityEngine;

namespace SelfDef.Variables
{
    public class PublicVariableReference : MonoBehaviour
    {
        public static PublicVariableReference Instance { get; private  set; }

        public PersistentVariables persistentVariable;
        public PlayerVariables playerVariable;
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
        }

    }
}
