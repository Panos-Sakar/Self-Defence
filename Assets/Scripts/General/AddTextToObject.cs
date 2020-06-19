using TMPro;
using UnityEngine;

namespace SelfDef.General
{

    public class AddTextToObject : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField]
        [Multiline]
        private string text;
        
#pragma warning restore CS0649
        // Start is called before the first frame update
        private void Start()
        {
            var canvas = new GameObject("TextMeshObject");
            canvas.transform.SetParent (this.GetComponent<Transform>());
            
            var canvasTextMesh = canvas.AddComponent<TextMeshPro>();
            canvasTextMesh.text = text;
            canvasTextMesh.fontSize = 5;
            canvasTextMesh.fontStyle = FontStyles.Bold;
            canvasTextMesh.alignment = TextAlignmentOptions.Midline;

            var transform1 = canvasTextMesh.transform;
            transform1.localEulerAngles = new Vector3(0, 90, 0);
            transform1.localPosition = new Vector3(-2f, 0f, 0f);
            
        }

    }
}
