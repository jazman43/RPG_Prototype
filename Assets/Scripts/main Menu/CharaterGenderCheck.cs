using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Menus
{
    public class CharaterGenderCheck : MonoBehaviour
    {
        [SerializeField] private bool isFemale = true;


        public bool IsGenderCheck()
        {
            return isFemale;
        }
    }

}
