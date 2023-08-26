using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana;

        [SerializeField] private GameObject manaUI = null;
        [SerializeField] private GameObject textManaUI = null;

        private Slider manaSlider;
        private Text manaText;

        private void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>();
                        
            manaSlider = manaUI.GetComponent<Slider>();

            manaText = textManaUI.GetComponent<Text>();
        }

        private void Update()
        {
            manaText.text = String.Format("{0:0}/{1:0}", mana.GetMana(), mana.GetMaxMana());

            manaSlider.value = mana.GetMana();
            manaSlider.maxValue = mana.GetMaxMana();
        }
    }
}