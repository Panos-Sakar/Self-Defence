using UnityEngine;

namespace Systems.Loading
{
    public class Initializer : MonoBehaviour
    {
#pragma warning disable CS0649
        public static Initializer Instance { get; private  set; }
    
        // Start is called before the first frame update
        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

#pragma warning restore CS0649
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
        
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            DontDestroyOnLoad(this.gameObject);
        }

        void OnMouseEnter()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
