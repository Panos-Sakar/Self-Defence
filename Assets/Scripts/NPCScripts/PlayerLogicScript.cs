using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerLogicScript : MonoBehaviour
{
    [SerializeField] private  float normFov = 50f, maxFov = 60f, minFov = 25f;
    [SerializeField] private float headRotationSpeed = 50f;
    
    public GameObject headInerTransform;
    public GameObject headGridTransform;
    private PlayerInputActions _inputActionsVar;
    private Camera _mainCamera;

    private float _zoomLevel;
    private float _fireProjectile;
    

    // Start is called before the first frame update
    void Awake()
    {
        _inputActionsVar = new PlayerInputActions();
        _inputActionsVar.PlayerControls.Fire.performed += ctx => _fireProjectile = ctx.ReadValue<float>();
        _inputActionsVar.PlayerControls.Zoom.performed += ctx => _zoomLevel = ctx.ReadValue<float>();
        _mainCamera = Camera.main;
        
        normFov = 50f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        headInerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        
        normFov += _zoomLevel;
        
        if (normFov < minFov) normFov = minFov;
        if (normFov > maxFov) normFov = maxFov;
        
        _mainCamera.fieldOfView = normFov;
    }

    private void OnEnable()
    {
        _inputActionsVar.Enable();
    }

    private void OnDisable()
    {
        _inputActionsVar.Disable();
    }
}