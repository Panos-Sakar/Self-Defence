using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraRotator : MonoBehaviour
{
    private PlayerInputActions _inputActionsVar;
    private Transform _myTransform;
    private Camera _mainCamera;
    
    [Header("Camera Properties")]
    [SerializeField] private float fieldOfView = 50f;
    [SerializeField] private float maxFieldOfView = 60f;
    [SerializeField] private float minFieldOfView = 25f;
    [SerializeField][Range(10,50)] private float rotateAmount = 30f;
    
    private float _mouseValue;
    private int _toggleRotate;
    private bool _canRotate;
    private float _zoomLevel;
    
    void Awake()
    {
        InitializeInputSystem();

        _myTransform = transform;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_canRotate)
        {
            _myTransform.transform.Rotate(new Vector3(0, _mouseValue, 0) * (Time.deltaTime * rotateAmount));
            UserInterfaceHandler.Instance.PrintToDebug(1,"Mouse Axis X Delta: " + _mouseValue);
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
        UserInterfaceHandler.Instance.PrintToDebug(2,"Toggle Rotate Value: " + _toggleRotate);
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
