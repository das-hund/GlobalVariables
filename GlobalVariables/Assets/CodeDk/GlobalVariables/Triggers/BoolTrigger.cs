using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by GlobalVariables of type bool.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/Bool Trigger")]
    public class BoolTrigger : GlobalVariableTriggerBase<BoolVariable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<bool> { }

        public TriggerEvent onValueChanged;
        // Like the event above, but passes an inverted value (true = false).
        public TriggerEvent onInvertedValueChanged;
        // Some extra events that are useful for callback events that can't be toggled.
        // e.g. Play() and Stop() methods can be called from OnTrue and OnFalse respectively.
        public UnityEvent onTrue;
        public UnityEvent onFalse;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            BoolVariable variable = source as BoolVariable;

            if (variable == null)
            {
                Debug.LogErrorFormat("Expected source to be BoolVariable, was {0}", source.GetType().Name);
                return;
            }

            // Invoke value changed UnityEvent.
            if (onValueChanged != null)
                onValueChanged.Invoke(variable.Value);

            // Invoke (inverted) value changed UnityEvent.
            if (onInvertedValueChanged != null)
                onInvertedValueChanged.Invoke(!variable.Value);

            // Invoke true or false UnityEvent.
            if (variable.Value)
            {
                if (onTrue != null)
                    onTrue.Invoke();
            }
            else
            {
                if (onFalse != null)
                    onFalse.Invoke();
            }
        }
    }
}
