// GENERATED AUTOMATICALLY FROM 'Assets/Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controller : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""7c32dae8-1944-4586-8905-812e458f4306"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bde98ef3-6929-4b7c-8f88-fb0a939c1ddf"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""889f6f81-c1fe-485b-a766-032356e5100f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpHold"",
                    ""type"": ""Button"",
                    ""id"": ""943758a0-40a5-4128-b282-71022564679d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""32fac9a9-2c44-4466-b869-4acf4a8ad30a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackHold"",
                    ""type"": ""Button"",
                    ""id"": ""e14b79da-0430-4891-9148-6a38ebfd9793"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""7d33955b-798e-481b-9a11-b8551aebe8e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""b4c3b9ba-47ad-436b-be1a-40b1c4414d2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""b67238b4-7904-4842-b439-4b27198ba80c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Stick_PS3_controller"",
                    ""id"": ""af26d9cc-94a7-4e1e-971a-b6113be847eb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d1a69896-fa99-4d05-a343-c20c4aa9e17d"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""88c7e11e-6a39-4e61-b6c4-2637f2ba9202"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Stick_Xbox_controller"",
                    ""id"": ""c55a0036-670e-4573-9e2b-33f003560aa6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2643b057-5573-4e29-8223-cc87a146bb6d"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9d3df271-2ddb-4627-9b76-3a2436cc40a0"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""D-pad_Xbox_controller"",
                    ""id"": ""c5482373-8cf8-4308-b596-76a39d94d1a4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4809d79d-6c7a-4930-8936-bd07abb30a44"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""01c2f3f5-cf0c-4455-834d-cd22f0f04f1d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6b4bb9d4-d049-4277-981d-8da29c380cca"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a747c16-b17b-4f8f-9091-e9c4a1cad8e5"",
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
                    ""id"": ""deb503de-7d88-4982-b3c9-653ad36e6bb4"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d41cad10-bb6c-4618-a145-753dd88342ef"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f08f1f44-961b-4015-baf2-25696ee4a3c9"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83ca3b4e-6c02-48a2-b79a-1e075a62e77f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a5a7ee7-c41a-4803-a5f4-f4fcc8fbb104"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5cc428b-b100-417e-8da5-b6980425fe52"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0ddde82-0635-46e6-950d-747c63eed544"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f517747b-32a6-49da-86cd-871aa4b41394"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16ee88c9-8d22-4dc4-b116-fdd8ba414970"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3944bc89-eb36-4902-a38a-df7fee8dbae1"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a68d63bc-dbb7-4889-b275-055a0076e0d3"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bbf1c21-2bed-4f63-a1ae-fde8c793b7f8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1683b861-fbac-49c3-9411-edfcab7c9bad"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef6d6381-5fde-425f-89df-10080a2217f9"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""bfd7a5c8-55b9-49c3-babe-46fc7a0f5595"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""PassThrough"",
                    ""id"": ""53892014-7c6e-416a-a60e-29bea59408d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""New action1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7ddce36e-126e-4745-8fae-af08414dcb56"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""78efbe1a-bb9b-41c3-9844-61492db36ed6"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_Move = m_InGame.FindAction("Move", throwIfNotFound: true);
        m_InGame_Jump = m_InGame.FindAction("Jump", throwIfNotFound: true);
        m_InGame_JumpHold = m_InGame.FindAction("JumpHold", throwIfNotFound: true);
        m_InGame_Attack = m_InGame.FindAction("Attack", throwIfNotFound: true);
        m_InGame_AttackHold = m_InGame.FindAction("AttackHold", throwIfNotFound: true);
        m_InGame_Block = m_InGame.FindAction("Block", throwIfNotFound: true);
        m_InGame_Back = m_InGame.FindAction("Back", throwIfNotFound: true);
        m_InGame_Start = m_InGame.FindAction("Start", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
        m_UI_Newaction1 = m_UI.FindAction("New action1", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_Move;
    private readonly InputAction m_InGame_Jump;
    private readonly InputAction m_InGame_JumpHold;
    private readonly InputAction m_InGame_Attack;
    private readonly InputAction m_InGame_AttackHold;
    private readonly InputAction m_InGame_Block;
    private readonly InputAction m_InGame_Back;
    private readonly InputAction m_InGame_Start;
    public struct InGameActions
    {
        private @Controller m_Wrapper;
        public InGameActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_InGame_Move;
        public InputAction @Jump => m_Wrapper.m_InGame_Jump;
        public InputAction @JumpHold => m_Wrapper.m_InGame_JumpHold;
        public InputAction @Attack => m_Wrapper.m_InGame_Attack;
        public InputAction @AttackHold => m_Wrapper.m_InGame_AttackHold;
        public InputAction @Block => m_Wrapper.m_InGame_Block;
        public InputAction @Back => m_Wrapper.m_InGame_Back;
        public InputAction @Start => m_Wrapper.m_InGame_Start;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnJump;
                @JumpHold.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnJumpHold;
                @JumpHold.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnJumpHold;
                @JumpHold.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnJumpHold;
                @Attack.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttack;
                @AttackHold.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackHold;
                @AttackHold.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackHold;
                @AttackHold.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackHold;
                @Block.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnBlock;
                @Back.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnBack;
                @Start.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @JumpHold.started += instance.OnJumpHold;
                @JumpHold.performed += instance.OnJumpHold;
                @JumpHold.canceled += instance.OnJumpHold;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @AttackHold.started += instance.OnAttackHold;
                @AttackHold.performed += instance.OnAttackHold;
                @AttackHold.canceled += instance.OnAttackHold;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Newaction;
    private readonly InputAction m_UI_Newaction1;
    public struct UIActions
    {
        private @Controller m_Wrapper;
        public UIActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
        public InputAction @Newaction1 => m_Wrapper.m_UI_Newaction1;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction1.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction1;
                @Newaction1.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction1;
                @Newaction1.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction1;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @Newaction1.started += instance.OnNewaction1;
                @Newaction1.performed += instance.OnNewaction1;
                @Newaction1.canceled += instance.OnNewaction1;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IInGameActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnJumpHold(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnAttackHold(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnNewaction1(InputAction.CallbackContext context);
    }
}
