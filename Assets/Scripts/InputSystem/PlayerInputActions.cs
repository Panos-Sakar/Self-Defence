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
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b6f6d40e-fa43-4f6c-af85-9edf0dba9cd5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""d4e385be-3374-42e5-9a68-5fe402ddd537"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
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
                    ""name"": ""ExitGame"",
                    ""type"": ""Button"",
                    ""id"": ""a345f31a-4076-4f09-afc0-a791eb2f30f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCursor"",
                    ""type"": ""PassThrough"",
                    ""id"": ""96bc3827-9944-437d-b958-6b9d7738dbea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5311ab8e-f088-450c-a815-4408306ca01a"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert,Clamp(min=-1,max=1),Scale(factor=5)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""be1333b4-3088-4982-941c-69a109267795"",
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
                    ""id"": ""ba0f0f27-a05f-4c10-a474-ae12c5b58013"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""85316cd9-9231-4d15-a26e-6c035d1cefc7"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2e6eef5b-cd84-4618-9476-3ed99ca29bb0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""ContextMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9380d59-68cd-4652-baee-9e354d71a7d4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db3e505a-2d2c-47f0-aca9-a827d0fb99a3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e23ed00-2683-4130-b01d-0e6eb23534d1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Zoom = m_PlayerControls.FindAction("Zoom", throwIfNotFound: true);
        m_PlayerControls_Fire = m_PlayerControls.FindAction("Fire", throwIfNotFound: true);
        m_PlayerControls_ContextMenu = m_PlayerControls.FindAction("ContextMenu", throwIfNotFound: true);
        m_PlayerControls_ExitGame = m_PlayerControls.FindAction("ExitGame", throwIfNotFound: true);
        m_PlayerControls_MoveCursor = m_PlayerControls.FindAction("MoveCursor", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerControls_Zoom;
    private readonly InputAction m_PlayerControls_Fire;
    private readonly InputAction m_PlayerControls_ContextMenu;
    private readonly InputAction m_PlayerControls_ExitGame;
    private readonly InputAction m_PlayerControls_MoveCursor;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Zoom => m_Wrapper.m_PlayerControls_Zoom;
        public InputAction @Fire => m_Wrapper.m_PlayerControls_Fire;
        public InputAction @ContextMenu => m_Wrapper.m_PlayerControls_ContextMenu;
        public InputAction @ExitGame => m_Wrapper.m_PlayerControls_ExitGame;
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
                @Zoom.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnZoom;
                @Fire.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @ContextMenu.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContextMenu;
                @ContextMenu.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContextMenu;
                @ContextMenu.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContextMenu;
                @ExitGame.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnExitGame;
                @ExitGame.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnExitGame;
                @ExitGame.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnExitGame;
                @MoveCursor.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveCursor;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @ContextMenu.started += instance.OnContextMenu;
                @ContextMenu.performed += instance.OnContextMenu;
                @ContextMenu.canceled += instance.OnContextMenu;
                @ExitGame.started += instance.OnExitGame;
                @ExitGame.performed += instance.OnExitGame;
                @ExitGame.canceled += instance.OnExitGame;
                @MoveCursor.started += instance.OnMoveCursor;
                @MoveCursor.performed += instance.OnMoveCursor;
                @MoveCursor.canceled += instance.OnMoveCursor;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnZoom(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnContextMenu(InputAction.CallbackContext context);
        void OnExitGame(InputAction.CallbackContext context);
        void OnMoveCursor(InputAction.CallbackContext context);
    }
}
