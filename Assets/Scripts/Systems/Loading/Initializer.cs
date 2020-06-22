using UnityEngine;

namespace SelfDef.Systems.Loading
{
    public class Initializer : MonoBehaviour
    {
#pragma warning disable CS0649
        public static Initializer Instance { get; private  set; }
        
        [SerializeField] private Texture2D cursorTexture;

        private const CursorMode CursorMode = UnityEngine.CursorMode.Auto;
        private readonly Vector2 _hotSpot = Vector2.zero;

#pragma warning restore CS0649
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
            DontDestroyOnLoad(this.gameObject);
            
            Cursor.SetCursor(cursorTexture, _hotSpot, CursorMode);
        }

        void OnMouseEnter()
        {
            Cursor.SetCursor(cursorTexture, _hotSpot, CursorMode);
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode);
        }
    }
}
