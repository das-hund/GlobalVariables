using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by any type of GlobalVariable and passes it's value as a string.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/String Trigger")]
    public class StringTrigger : GlobalVariableTriggerBase<StringVariable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<string> { }

        public TriggerEvent onValueChanged;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            StringVariable variable = source as StringVariable;

            if (variable == null)
            {
                Debug.LogErrorFormat("Expected source to be StringVariable, was {0}", source.GetType().Name);
                return;
            }

            if (onValueChanged != null)
                onValueChanged.Invoke(variable);
        }
    }
}
