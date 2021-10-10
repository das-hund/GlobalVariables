using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by any type of GlobalVariable and passes the GlobalVariable that trigged it.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/Global Variable Trigger")]
    public class GlobalVariableTrigger : GlobalVariableTriggerBase<GlobalVariable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<GlobalVariable> { }

        public TriggerEvent onValueChanged;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            if (onValueChanged != null)
                onValueChanged.Invoke(varEvent.Source);
        }
    }
}
