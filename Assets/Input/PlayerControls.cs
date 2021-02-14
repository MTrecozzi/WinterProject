// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Standard"",
            ""id"": ""da08dc2c-cb81-49e8-a9c7-2cd9dec6a5c4"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8120c488-4be7-4524-a7fa-86174b1dcfd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ControlStick"",
                    ""type"": ""Value"",
                    ""id"": ""ba2575ed-2402-46e0-b4b2-d61a3d94d3d9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""PassThrough"",
                    ""id"": ""870cdb04-1ffb-464c-8359-e2f87b9a50f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""AimStick"",
                    ""type"": ""Value"",
                    ""id"": ""fc732e54-d02a-4c82-bc58-627666c3fc32"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9d2ea4dc-03d6-44de-945a-4fdaa852359c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""AimDelta"",
                    ""type"": ""Value"",
                    ""id"": ""0b63a36f-87ba-45dd-9594-1da71855ed0e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e1467bb8-e9cf-4a22-bf6a-c3c91199969e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""996dfab8-309b-4733-b6e5-815946ed0394"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""817b4a4b-bad7-4a16-b4eb-447930833986"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e78defa-38ba-4e81-9efa-da019cffc781"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ed064ce-e1ad-425f-977d-2e2234dce93a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c4bae0b-9c10-4040-b9cc-c612a41831d5"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bd1e436-3827-4383-92d8-3ea4035c4e51"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27976095-dc11-4cd7-aca7-8263622d81b1"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9c7d01e-af50-4040-a0bd-03ce6d26dd85"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82b07954-ab34-4c48-9d58-1c6fa715e227"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb8db89d-487e-4a63-8c6b-b5fd4c8b844c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6bd8f49-3ae0-4aec-a2c4-361c3c36ad62"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=4,y=4)"",
                    ""groups"": """",
                    ""action"": ""AimDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35038767-8dc4-4a41-9872-880d15527dd4"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=4,y=4)"",
                    ""groups"": """",
                    ""action"": ""AimDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Standard
        m_Standard = asset.FindActionMap("Standard", throwIfNotFound: true);
        m_Standard_Jump = m_Standard.FindAction("Jump", throwIfNotFound: true);
        m_Standard_ControlStick = m_Standard.FindAction("ControlStick", throwIfNotFound: true);
        m_Standard_Dash = m_Standard.FindAction("Dash", throwIfNotFound: true);
        m_Standard_AimStick = m_Standard.FindAction("AimStick", throwIfNotFound: true);
        m_Standard_Shoot = m_Standard.FindAction("Shoot", throwIfNotFound: true);
        m_Standard_AimDelta = m_Standard.FindAction("AimDelta", throwIfNotFound: true);
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

    // Standard
    private readonly InputActionMap m_Standard;
    private IStandardActions m_StandardActionsCallbackInterface;
    private readonly InputAction m_Standard_Jump;
    private readonly InputAction m_Standard_ControlStick;
    private readonly InputAction m_Standard_Dash;
    private readonly InputAction m_Standard_AimStick;
    private readonly InputAction m_Standard_Shoot;
    private readonly InputAction m_Standard_AimDelta;
    public struct StandardActions
    {
        private @PlayerControls m_Wrapper;
        public StandardActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Standard_Jump;
        public InputAction @ControlStick => m_Wrapper.m_Standard_ControlStick;
        public InputAction @Dash => m_Wrapper.m_Standard_Dash;
        public InputAction @AimStick => m_Wrapper.m_Standard_AimStick;
        public InputAction @Shoot => m_Wrapper.m_Standard_Shoot;
        public InputAction @AimDelta => m_Wrapper.m_Standard_AimDelta;
        public InputActionMap Get() { return m_Wrapper.m_Standard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandardActions set) { return set.Get(); }
        public void SetCallbacks(IStandardActions instance)
        {
            if (m_Wrapper.m_StandardActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @ControlStick.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnControlStick;
                @ControlStick.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnControlStick;
                @ControlStick.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnControlStick;
                @Dash.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnDash;
                @AimStick.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnAimStick;
                @AimStick.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnAimStick;
                @AimStick.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnAimStick;
                @Shoot.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnShoot;
                @AimDelta.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnAimDelta;
                @AimDelta.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnAimDelta;
                @AimDelta.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnAimDelta;
            }
            m_Wrapper.m_StandardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @ControlStick.started += instance.OnControlStick;
                @ControlStick.performed += instance.OnControlStick;
                @ControlStick.canceled += instance.OnControlStick;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @AimStick.started += instance.OnAimStick;
                @AimStick.performed += instance.OnAimStick;
                @AimStick.canceled += instance.OnAimStick;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @AimDelta.started += instance.OnAimDelta;
                @AimDelta.performed += instance.OnAimDelta;
                @AimDelta.canceled += instance.OnAimDelta;
            }
        }
    }
    public StandardActions @Standard => new StandardActions(this);
    public interface IStandardActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnControlStick(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnAimStick(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnAimDelta(InputAction.CallbackContext context);
    }
}
