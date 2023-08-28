using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Control
{
    public interface IRaycastable
    {
        //Cursors GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}

