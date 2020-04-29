using Systems.UI;
using UnityEngine;

namespace Systems.FireProjectile
{
    public class RotateToMouse : MonoBehaviour
    {
#pragma warning disable CS0649
        
        private UnityEngine.Camera _mainCamera;
        private Ray _rayMouse;
        private RaycastHit _hit;
        private Vector3 _correctedPos;
        
        private Quaternion _rotation;

        [SerializeField] private float maximumLength = 100;
        [SerializeField] private LayerMask hitLayerMasks;
        void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
        }

#pragma warning restore CS0649
        // Update is called once per frame
        void Update()
        {
            var mousePos = Input.mousePosition;
            _rayMouse = _mainCamera.ScreenPointToRay(mousePos);
            
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
#if UNITY_EDITOR
                UserInterfaceHandler.Instance.PrintToDebug(2,"Mouse over UI Element");
#endif
            }
            else
            {
                UserInterfaceHandler.Instance.PrintToDebug(2,"");
            }
            if (Physics.Raycast(_rayMouse.origin, _rayMouse.direction, out _hit, hitLayerMasks))
            {
                _correctedPos = _hit.point - new Vector3(0, 1.95f, 0);
                RotateToMouseDirection(gameObject,_correctedPos);
            
#if UNITY_EDITOR
                UserInterfaceHandler.Instance.PrintToDebug(1,"RayCast: Hit -> " + _hit.transform.name + " | Pos -> " + _correctedPos);
#endif
            }
            else
            {
            
                _correctedPos = _rayMouse.GetPoint(maximumLength) - new Vector3(0, 2, 0);
                RotateToMouseDirection(gameObject,_correctedPos);
            
#if UNITY_EDITOR
                UserInterfaceHandler.Instance.PrintToDebug(1,"RayCast: Hit -> <Nothing>" + " | Pos -> " + _correctedPos);
#endif
            }

        }

        void RotateToMouseDirection(GameObject obj, Vector3 destination)
        {
            _rotation = Quaternion.LookRotation(destination);
            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, _rotation,1);
        }

        public Quaternion GetRotation()
        {
            return _rotation;
        }
    }
}
