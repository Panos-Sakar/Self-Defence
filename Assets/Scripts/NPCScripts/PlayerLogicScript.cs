using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerLogicScript : MonoBehaviour
{
    
    [SerializeField] private float headRotationSpeed = 50f;
    [SerializeField] private SpawnProjectiles projectileSystem;
    public GameObject headInnerTransform;
    public GameObject headGridTransform;
    
    private PlayerInputActions _inputActionsVar;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        InitializeInputSystem();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        headInnerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));

    }

    private void OnEnable()
    {
        _inputActionsVar.Enable();
    }

    private void OnDisable()
    {
        _inputActionsVar.Disable();
    }

    private void FireProjectile(InputAction.CallbackContext context)
    {
        projectileSystem.SpawnFireEffect();
    }

    private void InitializeInputSystem()
    {
        _inputActionsVar = new PlayerInputActions();
        _inputActionsVar.PlayerControls.Fire.performed += FireProjectile;
    }
}