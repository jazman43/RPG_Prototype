using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



namespace RPG.MainMenu.ChareterCreator
{
    public class ToggleColorPicker : MonoBehaviour
    {
        [System.Serializable] public class ChangeEvent : UnityEvent<string, Color> { }


        public ChangeEvent onChange = new ChangeEvent();

        private Toggle[] toggles;

        private void Awake()
        {
            toggles = GetComponentsInChildren<Toggle>();
        }

        private void OnEnable()
        {
            foreach (var toggle in toggles)
            {
                toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
        }

        private void OnToggleValueChanged(bool value)
        {

            if (value)
            {
                foreach (var toggle in toggles)
                {
                    if (toggle.isOn)
                    {
                        TriggerChangeEvent(toggle);
                        return;
                    }
                }
            }
        }

        private void OnDisable()
        {
            foreach (var toggle in toggles)
            {
                toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            }
        }


        protected virtual void TriggerChangeEvent(Toggle toggle)
        {
            Color color = Color.black;

            Image[] imgs = toggle.GetComponentsInChildren<Image>();
            foreach (Image img in imgs)
            {
                if (img.name == "Color Image")
                {
                    color = img.color;
                    break;
                }
            }
            // Invoke the on change event
            if (onChange != null)
                onChange.Invoke(toggle.name, color);
        }

    }
}

