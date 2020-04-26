using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInterfaceHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCanvas = null;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadeDelay = 0.1f;

    private int _animationIdCanvas = 0;
    private PlayerInputActions _inputActionsVar;
    private Transform _myTransform;

    // Start is called before the first frame update
    void Awake()
    {
        _myTransform = transform;
        InitializeInputSystem();
        mainCanvas.alpha = 0;
        _animationIdCanvas = LeanTween.alphaCanvas(mainCanvas, 1, fadeDuration).id;
        LeanTween.pause(_animationIdCanvas);
        StartCoroutine(FaidMainCanvasIn());
    }
    
    private IEnumerator FaidMainCanvasIn()
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

    private void ExitGame(InputAction.CallbackContext context)
    {
        Debug.Log("Exiting Game Now");
        Application.Quit();
    }
}
