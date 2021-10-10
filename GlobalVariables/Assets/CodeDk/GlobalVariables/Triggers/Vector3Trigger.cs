using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeDk
{
    /// <summary>
    /// UnityEvent that is triggered by GlobalVariables of type Vector3.
    /// </summary>
    [AddComponentMenu("No Brainer/Global Variables/Event Triggers/Vector 3 Trigger")]
    public class Vector3Trigger : GlobalVariableTriggerBase<Vector3Variable>
    {
        [Serializable]
        public class TriggerEvent : UnityEvent<Vector3> { }

        public TriggerEvent onValueChanged;

        protected override void OnVariableValueChanged(object source, GlobalVariableEvent varEvent)
        {
            Vector3Variable variable = source as Vector3Variable;

            if (variable == null)
            {
                Debug.LogErrorFormat("Expected source to be Vector3Variable, was {0}", source.GetType().Name);
                return;
            }

            if (onValueChanged != null)
                onValueChanged.Invoke(variable.Value);
        }
    }
}
