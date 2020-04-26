// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""48d4dae6-36e3-4267-84bf-f422026f8842"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""d4e385be-3374-42e5-9a68-5fe402ddd537"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ContextMenu"",
                    ""type"": ""Button"",
                    ""id"": ""1cdd8932-b0b4-400d-aad2-fc6c3f3bb6a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCursor"",
                    ""type"": ""PassThrough"",
                    ""id"": ""359c6df1-9048-4964-a58b-f95df0301adb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2e6eef5b-cd84-4618-9476-3ed99ca29bb0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dca7d7b9-dbb1-4e19-8f0e-642f89fea474"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f707476c-b736-4622-94e9-14073b31c969"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ContextMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db0747c2-cda5-449e-be92-badf782dcb95"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ContextMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41f05e00-a9e4-400e-abd3-4b976a6b7499"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraControls"",
            ""id"": ""a9bf0f48-25c3-4fc7-aab9-6e4f4e4d78eb"",
            ""actions"": [
                {
                    ""name"": ""RotateEnable"",
                    ""type"": ""Button"",
                    ""id"": ""41312b50-6b01-4c10-8120-cb6218a81a55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateDisable"",
                    ""type"": ""Button"",
                    ""id"": ""496929b5-7334-40f9-a0ec-7614b1a49bcb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7dc34ef5-c177-4ce8-a951-8dcc66ef4b95"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraWithMouse"",
                    ""type"": ""Value"",
                    ""id"": ""6cbb5035-4ae4-4a0a-9554-7b1aeeec72be"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraWithButtons"",
                    ""type"": ""Value"",
                    ""id"": ""e4c84ede-d756-448a-9af2-3335d1b91fef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ccdcf170-8904-4e41-b63c-db73849130e4"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RotateEnable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60f16807-178f-47f9-9c94-a93e8f8e999f"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RotateCameraWithMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""791e35cf-7570-4717-9016-25b6f2fdf0b2"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RotateDisable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""GamepadComp"",
                    ""id"": ""d4042add-92ef-4dac-9faa-ab4992874a57"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraWithButtons"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""40cf3d0f-23c3-41ae-89b6-e399ee888287"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCameraWithButtons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1941fbf3-fa37-45d7-94a3-05af24fd0fd6"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCameraWithButtons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASDComp"",
                    ""id"": ""cc951107-8e23-4b6d-a6b7-69b30da38200"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraWithButtons"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5222d06e-b37a-4c64-b7b6-3eed4252cedb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RotateCameraWithButtons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""61c68e75-1444-4f76-907d-cf4d5b28d113"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RotateCameraWithButtons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9c734609-6092-4b75-81e2-9f3f98e4b666"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert,Clamp(min=-1,max=1),Scale(factor=5)"",
                    ""groups"": ""Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""DamepadZoom"",
                    ""id"": ""73d5f80e-28f3-49f9-8104-b16eb50a0a19"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e328120d-cf8e-469a-bdc3-7bec4b58201c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3c7ddbf5-85c0-42b4-afc6-9d243a824526"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UIControls"",
            ""id"": ""5c4372d1-6c91-425a-9a59-3f7c69053871"",
            ""actions"": [
                {
                    ""name"": ""ExitGame"",
                    ""type"": ""Button"",
                    ""id"": ""8d1d5823-9315-41c1-adc2-a2f703e5b3fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8a133c47-ad4e-4432-ac4c-765e80916a52"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ExitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68af3607-6403-4cb8-bbc7-4e15025f930e"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ExitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Fire = m_PlayerControls.FindAction("Fire", throwIfNotFound: true);
        m_PlayerControls_ContextMenu = m_PlayerControls.FindAction("ContextMenu", throwIfNotFound: true);
        m_PlayerControls_MoveCursor = m_PlayerControls.FindAction("MoveCursor", throwIfNotFound: true);
        // CameraControls
        m_CameraControls = asset.FindActionMap("CameraControls", throwIfNotFound: true);
        m_CameraControls_RotateEnable = m_CameraControls.FindAction("RotateEnable", throwIfNotFound: true);
        m_CameraControls_RotateDisable = m_CameraControls.FindAction("RotateDisable", throwIfNotFound: true);
        m_CameraControls_Zoom = m_CameraControls.FindAction("Zoom", throwIfNotFound: true);
        m_CameraControls_RotateCameraWithMouse = m_CameraControls.FindAction("RotateCameraWithMouse", throwIfNotFound: true);
        m_CameraControls_RotateCameraWithButtons = m_CameraControls.FindAction("RotateCameraWithButtons", throwIfNotFound: true);
        // UIControls
        m_UIControls = asset.FindActionMap("UIControls", throwIfNotFound: true);
        m_UIControls_ExitGame = m_UIControls.FindAction("ExitGame", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Fire;
    private readonly InputAction m_PlayerControls_ContextMenu;
    private readonly InputAction m_PlayerControls_MoveCursor;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_PlayerControls_Fire;
        public InputAction @ContextMenu => m_Wrapper.m_PlayerControls_ContextMenu;
        public InputAction @MoveCursor => m_Wrapper.m_PlayerControls_MoveCursor;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @ContextMenu.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContextMenu;
                @ContextMenu.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContextMenu;
                @ContextMenu.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContextMenu;
                @MoveCursor.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveCursor;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @ContextMenu.started += instance.OnContextMenu;
                @ContextMenu.performed += instance.OnContextMenu;
                @ContextMenu.canceled += instance.OnContextMenu;
                @MoveCursor.started += instance.OnMoveCursor;
                @MoveCursor.performed += instance.OnMoveCursor;
                @MoveCursor.canceled += instance.OnMoveCursor;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // CameraControls
    private readonly InputActionMap m_CameraControls;
    private ICameraControlsActions m_CameraControlsActionsCallbackInterface;
    private readonly InputAction m_CameraControls_RotateEnable;
    private readonly InputAction m_CameraControls_RotateDisable;
    private readonly InputAction m_CameraControls_Zoom;
    private readonly InputAction m_CameraControls_RotateCameraWithMouse;
    private readonly InputAction m_CameraControls_RotateCameraWithButtons;
    public struct CameraControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public CameraControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotateEnable => m_Wrapper.m_CameraControls_RotateEnable;
        public InputAction @RotateDisable => m_Wrapper.m_CameraControls_RotateDisable;
        public InputAction @Zoom => m_Wrapper.m_CameraControls_Zoom;
        public InputAction @RotateCameraWithMouse => m_Wrapper.m_CameraControls_RotateCameraWithMouse;
        public InputAction @RotateCameraWithButtons => m_Wrapper.m_CameraControls_RotateCameraWithButtons;
        public InputActionMap Get() { return m_Wrapper.m_CameraControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICameraControlsActions instance)
        {
            if (m_Wrapper.m_CameraControlsActionsCallbackInterface != null)
            {
                @RotateEnable.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateEnable;
                @RotateEnable.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateEnable;
                @RotateEnable.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateEnable;
                @RotateDisable.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateDisable;
                @RotateDisable.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateDisable;
                @RotateDisable.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateDisable;
                @Zoom.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnZoom;
                @RotateCameraWithMouse.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateCameraWithMouse;
                @RotateCameraWithMouse.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateCameraWithMouse;
                @RotateCameraWithMouse.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateCameraWithMouse;
                @RotateCameraWithButtons.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateCameraWithButtons;
                @RotateCameraWithButtons.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateCameraWithButtons;
                @RotateCameraWithButtons.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnRotateCameraWithButtons;
            }
            m_Wrapper.m_CameraControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotateEnable.started += instance.OnRotateEnable;
                @RotateEnable.performed += instance.OnRotateEnable;
                @RotateEnable.canceled += instance.OnRotateEnable;
                @RotateDisable.started += instance.OnRotateDisable;
                @RotateDisable.performed += instance.OnRotateDisable;
                @RotateDisable.canceled += instance.OnRotateDisable;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @RotateCameraWithMouse.started += instance.OnRotateCameraWithMouse;
                @RotateCameraWithMouse.performed += instance.OnRotateCameraWithMouse;
                @RotateCameraWithMouse.canceled += instance.OnRotateCameraWithMouse;
                @RotateCameraWithButtons.started += instance.OnRotateCameraWithButtons;
                @RotateCameraWithButtons.performed += instance.OnRotateCameraWithButtons;
                @RotateCameraWithButtons.canceled += instance.OnRotateCameraWithButtons;
            }
        }
    }
    public CameraControlsActions @CameraControls => new CameraControlsActions(this);

    // UIControls
    private readonly InputActionMap m_UIControls;
    private IUIControlsActions m_UIControlsActionsCallbackInterface;
    private readonly InputAction m_UIControls_ExitGame;
    public struct UIControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ExitGame => m_Wrapper.m_UIControls_ExitGame;
        public InputActionMap Get() { return m_Wrapper.m_UIControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUIControlsActions instance)
        {
            if (m_Wrapper.m_UIControlsActionsCallbackInterface != null)
            {
                @ExitGame.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnExitGame;
                @ExitGame.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnExitGame;
                @ExitGame.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnExitGame;
            }
            m_Wrapper.m_UIControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ExitGame.started += instance.OnExitGame;
                @ExitGame.performed += instance.OnExitGame;
                @ExitGame.canceled += instance.OnExitGame;
            }
        }
    }
    public UIControlsActions @UIControls => new UIControlsActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnContextMenu(InputAction.CallbackContext context);
        void OnMoveCursor(InputAction.CallbackContext context);
    }
    public interface ICameraControlsActions
    {
        void OnRotateEnable(InputAction.CallbackContext context);
        void OnRotateDisable(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnRotateCameraWithMouse(InputAction.CallbackContext context);
        void OnRotateCameraWithButtons(InputAction.CallbackContext context);
    }
    public interface IUIControlsActions
    {
        void OnExitGame(InputAction.CallbackContext context);
    }
}
