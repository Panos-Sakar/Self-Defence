using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLogicScript : MonoBehaviour
{
    public float headRotationSpeed = 50f;
    
    public GameObject headInerTransform;
    public GameObject headGridTransform;

    private PlayerInputActions _inputActionsVar;

    private Vector2 _zoomLevel;
    private bool _fireProjectile;
    
    public float normFov = 50f, maxFov = 60f, minFov = 25f;

    // Start is called before the first frame update
    void Awake()
    {
        normFov = 50f;
        _inputActionsVar = new PlayerInputActions();
        _inputActionsVar.PlayerControls.Fire.performed += ctx => _fireProjectile = ctx.ReadValue<bool>();
        _inputActionsVar.PlayerControls.Zoom.performed += ctx => _zoomLevel = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        headInerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        normFov += _zoomLevel.y/100;
        if (normFov < minFov) normFov = minFov;
        if (normFov > maxFov) normFov = maxFov;
        Camera.main.fieldOfView = normFov;
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