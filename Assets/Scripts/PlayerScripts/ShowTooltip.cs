using SelfDef.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SelfDef.PlayerScripts
{
    public class ShowTooltip : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField]
        private Canvas canvas;

        [SerializeField] 
        private TextMeshProUGUI label;
        [SerializeField] 
        private TextMeshProUGUI cost;
        [SerializeField] 
        private Image icon;

        [SerializeField] 
        private RectTransform background;

        [SerializeField] 
        private Camera myCamera;

        [SerializeField] 
        private GameObject tooltipPos;

        private bool _show;
        private bool _newObject;
        private GameObject _targetObject;

        private string _textTpPrint;
        private string _costToPrint;
        private Sprite _imageToPrint;
        private Vector3 _worldPosition;
        
#pragma warning restore CS0649

        private void Start()
        {
            DisableTooltip();
        }

        public void DisableTooltip()
        {
            _show = false;
            canvas.enabled = false;

        }
        public void EnableTooltip(Transform obj)
        {
            _targetObject = obj.gameObject;
            _newObject = true;
            _show = true;
        }

        private void Update()
        {
            if(!_show) return;
            
            if (_newObject)
            {
                MakeText();
                canvas.enabled = true;
                label.text = _textTpPrint;
                background.sizeDelta = new Vector2(label.preferredWidth ,5);
                cost.text = _costToPrint;
                _worldPosition = _targetObject.transform.position + new Vector3(0,5,0);
                tooltipPos.transform.position = _worldPosition;
                icon.sprite = _imageToPrint;
                _newObject = false;
            }
            tooltipPos.transform.LookAt(myCamera.transform);


        }

        private void MakeText()
        {
            _textTpPrint = "Error";
            var canChangeSettingsComp = _targetObject.GetComponent<ICanChangeSettings>();
            if ( canChangeSettingsComp != null)
            {
                _textTpPrint = canChangeSettingsComp.TipText;
                _imageToPrint = canChangeSettingsComp.GetIcon();
            }

            _costToPrint = "";
            var giveUpgradeComp =_targetObject.GetComponent<IGiveUpgrade>();
            if (giveUpgradeComp != null)
            {
                _costToPrint += $"Cost: {giveUpgradeComp.Cost}";
            }
        }
    }
}
