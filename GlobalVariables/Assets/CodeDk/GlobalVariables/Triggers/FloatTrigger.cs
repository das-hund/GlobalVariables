using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by any type of GlobalVariable and passes it's value as a float.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/Float Trigger")]
    public class FloatTrigger : GlobalVariableTriggerBase<FloatVariable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<float> { }

        public TriggerEvent onValueChanged;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            FloatVariable variable = source as FloatVariable;

            if (variable == null)
            {
                Debug.LogErrorFormat("Expected source to be FloatVariable, was {0}", source.GetType().Name);
                return;
            }

            if (onValueChanged != null)
                onValueChanged.Invoke(variable.Value);
        }
    }
}
