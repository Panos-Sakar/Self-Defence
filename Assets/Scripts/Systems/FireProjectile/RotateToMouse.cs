using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    private Camera _mainCamera;
    private Ray _rayMouse;

    private Vector3 _position;
    private Vector3 _direction;
    private Quaternion _rotation;

    [SerializeField] private float maximumLength;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var mousePos = Input.mousePosition;
        _rayMouse = _mainCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(_rayMouse.origin, _rayMouse.direction, out hit, maximumLength))
        {
            RotateToMouseDirection(gameObject,hit.point);
        }
        else
        {
            var pos = _rayMouse.GetPoint(maximumLength);
            RotateToMouseDirection(gameObject,pos);
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
