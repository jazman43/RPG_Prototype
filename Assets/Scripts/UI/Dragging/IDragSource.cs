using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core.UI.Dragging
{
    
    public interface IDragSource<T> where T : class
    {
       
        T GetItem();

        
        int GetNumber();

        
        void RemoveItems(int number);
    }
}