using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Linear Function", menuName = "Global Variables/Functions/Linear")]
    public class LinearFunction : ScriptableObject
    {
        public FloatReference parameter;

        public FloatReference constant;
        public FloatReference scalar;

        public FloatVariable result;

        public void OnEnable()
        {
            SubscribeToEvents();
        }

        public void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void OnValidate()
        {
            UpdateResult(null, VariableReferenceEvent.Empty);
        }

        private void UpdateResult(object subject, VariableReferenceEvent args)
        {
            if (result == null ||
                !parameter.IsValid ||
                !scalar.IsValid ||
                !constant.IsValid)
            {
                return;
            }

            result.Value = parameter.Value * scalar.Value + constant.Value;
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            parameter?.SubscribeToEvents(UpdateResult);
            constant?.SubscribeToEvents(UpdateResult);
            scalar?.SubscribeToEvents(UpdateResult);
        }

        private void UnsubscribeFromEvents()
        {
            parameter?.UnsubscribeFromEvents(UpdateResult);
            constant?.UnsubscribeFromEvents(UpdateResult);
            scalar?.UnsubscribeFromEvents(UpdateResult);
        }
    }
}