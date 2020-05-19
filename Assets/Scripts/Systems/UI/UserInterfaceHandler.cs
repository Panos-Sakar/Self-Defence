using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Systems.UI
{
    public class UserInterfaceHandler : MonoBehaviour
    {
#pragma warning disable CS0649
        public static UserInterfaceHandler Instance { get; private  set; }

        [Header("References")]
        [SerializeField] private List<TextMeshProUGUI> debugTextFields;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider staminaBar;
        [SerializeField] private TextMeshProUGUI titleField;
        [SerializeField] private TextMeshProUGUI moneyField;
        [SerializeField] private Image gamepadImage;
        [SerializeField] private Image keyboardAndMouseImage;
        [SerializeField] private List<Button> upgradeButtons;

        private PlayerInputActions _inputActionsVar;
        private CanvasGroup _mainCanvas;
    
        [Header("Properties")]
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float fadeDelay = 0.1f;
    
        [Header("Cashing")]
        private Transform _myTransform;

        [Header("Private variables")]
        private int _animationIdCanvas;

#pragma warning restore CS0649
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

            _mainCanvas = gameObject.GetComponent<CanvasGroup>();
        
            //Initialize Fields
            InitializeInputSystem();
            _mainCanvas.alpha = 0;

            //Create Tween Animations
            _animationIdCanvas = LeanTween.alphaCanvas(_mainCanvas, 1, fadeDuration).id;
            LeanTween.pause(_animationIdCanvas);
        
            //Start Tween animations
            StartCoroutine(FadeMainCanvas());
        
        }
    
        private IEnumerator FadeMainCanvas()
        {
            yield return new WaitForSeconds(fadeDelay);
            LeanTween.resume(_animationIdCanvas);
        }
    
        private void InitializeInputSystem()
        {
            _inputActionsVar = new PlayerInputActions();
            _inputActionsVar.UIControls.ExitGame.performed += ExitGame;
        }
    
        private void OnEnable()
        {
            _inputActionsVar.Enable();
        }

        private void OnDisable()
        {
            _inputActionsVar.Disable();
        }

        private static void ExitGame(InputAction.CallbackContext context)
        {
            Application.Quit();
        }

        public void PrintToDebug(int pos, string text)
        {
            if (pos >= 0 && pos <= debugTextFields.Count)
            {
                debugTextFields[pos].text = text;
            }
        }
        public void ToggleInputIcon(string inputScheme)
        {
            if (keyboardAndMouseImage.IsActive())
            {
                keyboardAndMouseImage.enabled = false;
                gamepadImage.enabled = true;

            }
            else if (gamepadImage.IsActive())
            {
                keyboardAndMouseImage.enabled = true;
                gamepadImage.enabled = false;
            }
            else
            {
                keyboardAndMouseImage.enabled = true;
                gamepadImage.enabled = false;
            }
        }

        public void ActivateButton(string buttonName)
        {
            foreach (Button button in upgradeButtons)
            {
                if (button.name == buttonName)
                {
                    button.interactable = true;
                }
            }
        }
    }
}
