using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    private Camera _mainCamera = null;
    private Ray _rayMouse;
    private RaycastHit hit;
    private Vector3 _correctedPos;

    private Vector3 _position = new Vector3(0,0,0);
    private Vector3 _direction = new Vector3(0,0,0);
    private Quaternion _rotation = new Quaternion();

    [SerializeField] private float maximumLength = 100;
    [SerializeField] private LayerMask hitLayerMasks;
    void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        _rayMouse = _mainCamera.ScreenPointToRay(mousePos);
        UserInterfaceHandler.Instance.PrintToDebug(8,_rayMouse.ToString());
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            UserInterfaceHandler.Instance.PrintToDebug(7,"over game object");
        }
        else
        {
            UserInterfaceHandler.Instance.PrintToDebug(7,"");
        }
        if (Physics.Raycast(_rayMouse.origin, _rayMouse.direction, out hit, maximumLength, hitLayerMasks))
        {
            _correctedPos = hit.point - new Vector3(0, 2, 0);
            RotateToMouseDirection(gameObject,_correctedPos);
            
            #if UNITY_EDITOR
            UserInterfaceHandler.Instance.PrintToDebug(0,"RayCast: Hit -> " + hit.transform.name + " | Pos -> " + _correctedPos);
            #endif
        }
        else
        {
            
            _correctedPos = _rayMouse.GetPoint(maximumLength) - new Vector3(0, 2, 0);
            RotateToMouseDirection(gameObject,_correctedPos);
            
            #if UNITY_EDITOR
            UserInterfaceHandler.Instance.PrintToDebug(0,"RayCast: Hit -> <Nothing>" + " | Pos -> " + _correctedPos);
            #endif
        }

    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        _direction = destination - obj.transform.position;
        _rotation = Quaternion.LookRotation(destination);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, _rotation,1);
    }

    public Quaternion GetRotation()
    {
        return _rotation;
    }
}
