using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by any type of GlobalVariable and passes it's value as an integer.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/Int Trigger")]
    public class IntTrigger : GlobalVariableTriggerBase<IntVariable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<int> { }

        public TriggerEvent onValueChanged;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            IntVariable variable = source as IntVariable;

            if (variable == null)
            {
                Debug.LogErrorFormat("Expected source to be IntVariable, was {0}", source.GetType().Name);
                return;
            }

            if (onValueChanged != null)
                onValueChanged.Invoke(variable.Value);
        }
    }
}
