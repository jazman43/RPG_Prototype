using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jareds.Utils;
using RPG.Saving;
using RPG.core;


namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;

        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        EnableDisableCamMovment camMovment;

        bool isChoosing = false;

        public event Action onConversationUpdated;

        private void Awake()
        {
            camMovment = GameObject.FindGameObjectWithTag("Core").GetComponent<EnableDisableCamMovment>();
        }


        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            if(newDialogue!= null)
            {
                currentConversant = newConversant;
                currentDialogue = newDialogue;
                currentNode = currentDialogue.GetRootNode();
                TriggerEnterAction();
                Debug.Log("convo Start" + camMovment);
                //unlock cursor
                Cursor.lockState = CursorLockMode.Confined;
                if (camMovment.EnableDisable()) { camMovment.EnableDisable(); }

                onConversationUpdated?.Invoke();

                
            }
            
        }

        public void Quit()
        {
            currentDialogue = null;
            TriggerExitAction();
            currentNode = null;
            isChoosing = false;
            currentConversant = null;

            Cursor.lockState = CursorLockMode.Locked;
            camMovment.EnableDisable();

            onConversationUpdated();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public string GetCurrentConversantName()
        {
            if (isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetName();
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }
    
        public void Next()
        {
            int numPlayerResponses = FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }

            DialogueNode[] children = FilterOnCondition(currentDialogue.GetAIChildren(currentNode)).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[randomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public void SetPlayerName(string _playerName)
        {
            playerName = _playerName;
        }
    }
}