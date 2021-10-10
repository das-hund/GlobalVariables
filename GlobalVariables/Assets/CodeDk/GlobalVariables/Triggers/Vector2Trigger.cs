using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by GlobalVariables of type Vector2.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/Vector 2 Trigger")]
    public class Vector2Trigger : GlobalVariableTriggerBase<Vector2Variable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<Vector2> { }

        public TriggerEvent onValueChanged;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            Vector2Variable variable = source as Vector2Variable;

            if (variable == null)
            {
                Debug.LogErrorFormat("Expected source to be Vector2Variable, was {0}", source.GetType().Name);
                return;
            }

            if (onValueChanged != null)
                onValueChanged.Invoke(variable.Value);
        }
    }
}
