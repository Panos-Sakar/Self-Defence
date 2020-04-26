using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotator : MonoBehaviour
{
    private Transform _myTransform;
    private PlayerInputActions _inputActionsVar;
    private readonly List<TextMeshProUGUI> _uiDebugTextMeshPro = new List<TextMeshProUGUI>();
    private GameObject _temp;
    private GameObject _uiDebug;
    private Camera _mainCamera;
    
    [SerializeField] private float fieldOfView = 50f;
    [SerializeField] private float maxFieldOfView = 60f;
    [SerializeField] private float minFieldOfView = 25f;
    
    [SerializeField][Range(10,50)] private float rotateAmount = 30f;
    private float _mouseValue = 0f;
    private int _toggleRotate = 0;
    private bool _canRotate = false;
    private float _zoomLevel = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        InitializeInputSystem();
        
        _uiDebug = GameObject.FindGameObjectWithTag("UIDebug");
        foreach (Transform child in _uiDebug.transform)
        {
            _temp = child.gameObject;
            _uiDebugTextMeshPro.Add(_temp.GetComponent<TextMeshProUGUI>());
        }

        _myTransform = transform;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_canRotate)
        {
            _myTransform.transform.Rotate(new Vector3(0, _mouseValue, 0) * (Time.deltaTime * rotateAmount));
            _uiDebugTextMeshPro[0].text = "Mouse Axis X Delta: " + _mouseValue;
        }else 
        if (_toggleRotate != 0)
        {
            _myTransform.transform.Rotate(new Vector3(0, _toggleRotate, 0) * (Time.deltaTime * rotateAmount*2.5f));
        }

        fieldOfView += _zoomLevel;
        
        if (fieldOfView < minFieldOfView) fieldOfView = minFieldOfView;
        if (fieldOfView > maxFieldOfView) fieldOfView = maxFieldOfView;
        
        _mainCamera.fieldOfView = fieldOfView;
    }

    private void RotateCamera(InputAction.CallbackContext context)
    {
        _mouseValue = context.ReadValue<float>();
    }

    private void StartCameraRotation(InputAction.CallbackContext context)
    {
        _canRotate = true;
    }
    private void StopCameraRotation(InputAction.CallbackContext context)
    {
        _canRotate = false;
    }
    
    private void ZoomRelativeToPlayer(InputAction.CallbackContext context)
    {
        _zoomLevel = context.ReadValue<float>();
    }

    private void ToggleRotate(InputAction.CallbackContext context)
    {
        _toggleRotate = (int)context.ReadValue<float>();
        _uiDebugTextMeshPro[1].text = "Toggle Rotate Value: " + _toggleRotate;
    }
    private void OnEnable()
    {
        _inputActionsVar.Enable();
    }

    private void OnDisable()
    {
        _inputActionsVar.Disable();
    }

    private void InitializeInputSystem()
    {
        _inputActionsVar = new PlayerInputActions();
        _inputActionsVar.CameraControls.RotateCameraWithMouse.performed += RotateCamera;
        _inputActionsVar.CameraControls.RotateCameraWithButtons.performed += ToggleRotate;
        _inputActionsVar.CameraControls.RotateEnable.performed += StartCameraRotation;
        _inputActionsVar.CameraControls.RotateDisable.performed += StopCameraRotation;
        _inputActionsVar.CameraControls.Zoom.performed += ZoomRelativeToPlayer;
    }
}
