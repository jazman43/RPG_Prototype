using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction behaviour)
        {
            if (currentAction == behaviour)
            {
                return;
            }
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = behaviour;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }

}
