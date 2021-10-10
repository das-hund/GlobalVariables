using UnityEngine;
using UnityEngine.Serialization;

namespace CodeDk
{
    /// <summary>
    /// Base class used to implement custom UnityEvents that are triggered by GlobalVariables.
    /// </summary>
    /// <typeparam name="GlobalVariableType">The type of GlobalVariable this event is trigged by.</typeparam>
    public abstract class GlobalVariableTriggerBase<GlobalVariableType> : MonoBehaviour
        where GlobalVariableType : GlobalVariable
    {
        [Tooltip("Trigger upon startup. Useful for initializing event listeners.")]
        public bool TriggerOnAwake = false;
        [Tooltip("Trigger whenever this GameObject becomes active. Useful for updating event listeners.")]
        public bool TriggerOnEnable = false;
        [Tooltip("The global variable used for this trigger")]
        [FormerlySerializedAs("TargetVariable")]
        public GlobalVariableType variable;

        protected virtual void Awake()
        {
            if (!variable)
                return;

            if (TriggerOnAwake)
                OnVariableValueChanged(variable, GlobalVariableEvent.Empty);
        }

        protected virtual void OnEnable()
        {
            if (!variable)
                return;

            // Subscribe to change event.
            variable.ChangedEvent += OnVariableValueChanged;

            if (TriggerOnEnable)
                OnVariableValueChanged(variable, GlobalVariableEvent.Empty);
        }

        protected virtual void OnDisable()
        {
            if (!variable)
                return;

            // Unsubscribe from change event (prevents memory leaks).
            variable.ChangedEvent -= OnVariableValueChanged;
        }

        void OnValidate()
        {
            if (!variable)
                return;

            // Raise event when anything is modified in the inspector and GameObject is active.
            if (gameObject.activeInHierarchy)
                OnVariableValueChanged(variable, GlobalVariableEvent.Empty);
        }

        /// <summary>
        /// Called when the targeted GlobalVariable's value changes.
        /// </summary>
        /// <param name="globalVar">Global Variable</param>
        protected virtual void OnVariableValueChanged(object source, GlobalVariableEvent globalVar)
        {
            // Override with your own custom UnityEvent in the derived class.
        }
    }
}