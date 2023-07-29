//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Inputs/MyInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace RPG.Inputs
{
    public partial class @MyInputs: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MyInputs()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInputs"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""74261640-8df6-4bf2-875d-05356f8c1f38"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""dad003e2-8034-4471-9a1e-1d126f14ae47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveCam"",
                    ""type"": ""Value"",
                    ""id"": ""806c3f1a-cc29-4d06-ad76-3db02c29ec4a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""mousePos"",
                    ""type"": ""Value"",
                    ""id"": ""50248898-6059-4593-b7d1-1a13c0fb8b12"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TEST_SAVE"",
                    ""type"": ""Button"",
                    ""id"": ""ab0c9073-896c-4532-a509-a1ac50e75af8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TEST_LOAD"",
                    ""type"": ""Button"",
                    ""id"": ""d2423226-377b-4a22-b86e-3e79e11cfd1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dc81924e-40ee-40f6-83ed-8f9c8134d7bd"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0f92665-67dc-40b4-b4c5-cf95975cfb3e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""7ba662b4-4510-4ec7-9cc3-d63acef14083"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveCam"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7454e79b-92eb-4c96-8c06-9412f877af5b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""moveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b6b59be5-1701-46fd-8229-d9a8b1d4af15"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""moveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1cb5c640-9758-4b04-a8d5-ca74a4c4a5c4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""moveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""72694282-ad76-4fdd-aebe-cc8bed906265"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""moveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""42bbb057-04c5-413a-bc26-5452521388b5"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""mousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce6659db-6b4c-40b7-9f32-5168e04e03b1"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""TEST_SAVE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0803815b-50b4-4459-b737-8957bac6f403"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard & mouse"",
                    ""action"": ""TEST_LOAD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard & mouse"",
            ""bindingGroup"": ""KeyBoard & mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // PlayerActions
            m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
            m_PlayerActions_Move = m_PlayerActions.FindAction("Move", throwIfNotFound: true);
            m_PlayerActions_moveCam = m_PlayerActions.FindAction("moveCam", throwIfNotFound: true);
            m_PlayerActions_mousePos = m_PlayerActions.FindAction("mousePos", throwIfNotFound: true);
            m_PlayerActions_TEST_SAVE = m_PlayerActions.FindAction("TEST_SAVE", throwIfNotFound: true);
            m_PlayerActions_TEST_LOAD = m_PlayerActions.FindAction("TEST_LOAD", throwIfNotFound: true);
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // PlayerActions
        private readonly InputActionMap m_PlayerActions;
        private List<IPlayerActionsActions> m_PlayerActionsActionsCallbackInterfaces = new List<IPlayerActionsActions>();
        private readonly InputAction m_PlayerActions_Move;
        private readonly InputAction m_PlayerActions_moveCam;
        private readonly InputAction m_PlayerActions_mousePos;
        private readonly InputAction m_PlayerActions_TEST_SAVE;
        private readonly InputAction m_PlayerActions_TEST_LOAD;
        public struct PlayerActionsActions
        {
            private @MyInputs m_Wrapper;
            public PlayerActionsActions(@MyInputs wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_PlayerActions_Move;
            public InputAction @moveCam => m_Wrapper.m_PlayerActions_moveCam;
            public InputAction @mousePos => m_Wrapper.m_PlayerActions_mousePos;
            public InputAction @TEST_SAVE => m_Wrapper.m_PlayerActions_TEST_SAVE;
            public InputAction @TEST_LOAD => m_Wrapper.m_PlayerActions_TEST_LOAD;
            public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerActionsActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @moveCam.started += instance.OnMoveCam;
                @moveCam.performed += instance.OnMoveCam;
                @moveCam.canceled += instance.OnMoveCam;
                @mousePos.started += instance.OnMousePos;
                @mousePos.performed += instance.OnMousePos;
                @mousePos.canceled += instance.OnMousePos;
                @TEST_SAVE.started += instance.OnTEST_SAVE;
                @TEST_SAVE.performed += instance.OnTEST_SAVE;
                @TEST_SAVE.canceled += instance.OnTEST_SAVE;
                @TEST_LOAD.started += instance.OnTEST_LOAD;
                @TEST_LOAD.performed += instance.OnTEST_LOAD;
                @TEST_LOAD.canceled += instance.OnTEST_LOAD;
            }

            private void UnregisterCallbacks(IPlayerActionsActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @moveCam.started -= instance.OnMoveCam;
                @moveCam.performed -= instance.OnMoveCam;
                @moveCam.canceled -= instance.OnMoveCam;
                @mousePos.started -= instance.OnMousePos;
                @mousePos.performed -= instance.OnMousePos;
                @mousePos.canceled -= instance.OnMousePos;
                @TEST_SAVE.started -= instance.OnTEST_SAVE;
                @TEST_SAVE.performed -= instance.OnTEST_SAVE;
                @TEST_SAVE.canceled -= instance.OnTEST_SAVE;
                @TEST_LOAD.started -= instance.OnTEST_LOAD;
                @TEST_LOAD.performed -= instance.OnTEST_LOAD;
                @TEST_LOAD.canceled -= instance.OnTEST_LOAD;
            }

            public void RemoveCallbacks(IPlayerActionsActions instance)
            {
                if (m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerActionsActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerActionsActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
        private int m_KeyBoardmouseSchemeIndex = -1;
        public InputControlScheme KeyBoardmouseScheme
        {
            get
            {
                if (m_KeyBoardmouseSchemeIndex == -1) m_KeyBoardmouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoard & mouse");
                return asset.controlSchemes[m_KeyBoardmouseSchemeIndex];
            }
        }
        private int m_GamePadSchemeIndex = -1;
        public InputControlScheme GamePadScheme
        {
            get
            {
                if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
                return asset.controlSchemes[m_GamePadSchemeIndex];
            }
        }
        public interface IPlayerActionsActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnMoveCam(InputAction.CallbackContext context);
            void OnMousePos(InputAction.CallbackContext context);
            void OnTEST_SAVE(InputAction.CallbackContext context);
            void OnTEST_LOAD(InputAction.CallbackContext context);
        }
    }
}
