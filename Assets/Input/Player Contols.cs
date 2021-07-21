// GENERATED AUTOMATICALLY FROM 'Assets/Input/Player Contols.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerContols : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerContols()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Contols"",
    ""maps"": [
        {
            ""name"": ""Player Controls A"",
            ""id"": ""cd5ed86b-243f-43b5-88e7-a0d2ab07fc63"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""afd37814-4069-429a-b6c8-afed78b83e59"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""667bba5b-35dc-4ceb-b99e-3c390ef2752e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""19c74244-f54e-4dce-b454-58da36e3b497"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""9ac59354-a8c4-4e90-b074-c45f433d5924"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Super"",
                    ""type"": ""Button"",
                    ""id"": ""1b4597de-160a-4ea0-adf3-ddfe01755486"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""2386121b-7d30-4540-9c4c-1a3516ab1e37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""675b3d56-9f8a-49f5-8aa4-2f747b859fb0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e2a2fd0-7b48-4d78-8817-df2a2df18f3e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a5814cb-224d-4d1d-a500-4e78a65da0dd"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f145623e-4368-413b-8da4-2a79abf7b637"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ff83ec5-d72f-484a-a5bf-c3715f9a5fbf"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1a139ca-7ce2-4eba-af2c-4e599151bf3d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Super"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f5cf186-ea0b-4cbf-bab7-318ea45fddb4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu Controls"",
            ""id"": ""18389432-e66d-4561-ace7-9cc9ef833c3b"",
            ""actions"": [
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""706785f1-7e81-441d-8607-ce274c939985"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e5ea139c-b8ba-4342-a6ec-0e12b947b9d1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfb1f5c4-d7b5-4fe1-8ada-70a7753b8079"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
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
        // Player Controls A
        m_PlayerControlsA = asset.FindActionMap("Player Controls A", throwIfNotFound: true);
        m_PlayerControlsA_Movement = m_PlayerControlsA.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControlsA_Grab = m_PlayerControlsA.FindAction("Grab", throwIfNotFound: true);
        m_PlayerControlsA_Throw = m_PlayerControlsA.FindAction("Throw", throwIfNotFound: true);
        m_PlayerControlsA_Dodge = m_PlayerControlsA.FindAction("Dodge", throwIfNotFound: true);
        m_PlayerControlsA_Super = m_PlayerControlsA.FindAction("Super", throwIfNotFound: true);
        m_PlayerControlsA_Pause = m_PlayerControlsA.FindAction("Pause", throwIfNotFound: true);
        // Menu Controls
        m_MenuControls = asset.FindActionMap("Menu Controls", throwIfNotFound: true);
        m_MenuControls_Start = m_MenuControls.FindAction("Start", throwIfNotFound: true);
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

    // Player Controls A
    private readonly InputActionMap m_PlayerControlsA;
    private IPlayerControlsAActions m_PlayerControlsAActionsCallbackInterface;
    private readonly InputAction m_PlayerControlsA_Movement;
    private readonly InputAction m_PlayerControlsA_Grab;
    private readonly InputAction m_PlayerControlsA_Throw;
    private readonly InputAction m_PlayerControlsA_Dodge;
    private readonly InputAction m_PlayerControlsA_Super;
    private readonly InputAction m_PlayerControlsA_Pause;
    public struct PlayerControlsAActions
    {
        private @PlayerContols m_Wrapper;
        public PlayerControlsAActions(@PlayerContols wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerControlsA_Movement;
        public InputAction @Grab => m_Wrapper.m_PlayerControlsA_Grab;
        public InputAction @Throw => m_Wrapper.m_PlayerControlsA_Throw;
        public InputAction @Dodge => m_Wrapper.m_PlayerControlsA_Dodge;
        public InputAction @Super => m_Wrapper.m_PlayerControlsA_Super;
        public InputAction @Pause => m_Wrapper.m_PlayerControlsA_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControlsA; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsAActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsAActions instance)
        {
            if (m_Wrapper.m_PlayerControlsAActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnMovement;
                @Grab.started -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnGrab;
                @Throw.started -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnThrow;
                @Dodge.started -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnDodge;
                @Super.started -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnSuper;
                @Super.performed -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnSuper;
                @Super.canceled -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnSuper;
                @Pause.started -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsAActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerControlsAActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Super.started += instance.OnSuper;
                @Super.performed += instance.OnSuper;
                @Super.canceled += instance.OnSuper;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerControlsAActions @PlayerControlsA => new PlayerControlsAActions(this);

    // Menu Controls
    private readonly InputActionMap m_MenuControls;
    private IMenuControlsActions m_MenuControlsActionsCallbackInterface;
    private readonly InputAction m_MenuControls_Start;
    public struct MenuControlsActions
    {
        private @PlayerContols m_Wrapper;
        public MenuControlsActions(@PlayerContols wrapper) { m_Wrapper = wrapper; }
        public InputAction @Start => m_Wrapper.m_MenuControls_Start;
        public InputActionMap Get() { return m_Wrapper.m_MenuControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuControlsActions instance)
        {
            if (m_Wrapper.m_MenuControlsActionsCallbackInterface != null)
            {
                @Start.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_MenuControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
            }
        }
    }
    public MenuControlsActions @MenuControls => new MenuControlsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsAActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnSuper(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMenuControlsActions
    {
        void OnStart(InputAction.CallbackContext context);
    }
}
