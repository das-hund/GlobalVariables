using UnityEngine;

namespace CodeDk
{
    public abstract class LinearFunction : ScriptableObject
    {
    }

    public abstract class LinearFunction<TVar, TGlobalVar, TVarReference> : LinearFunction
        where TGlobalVar : GlobalVariable<TVar>
        where TVarReference : VariableReference<TVar, TGlobalVar>
    {
        [Tooltip("The input variable."), SerializeField]
        private TVarReference _parameter;

        [Tooltip("The factor the input will be multiplied with."), SerializeField]
        private TVarReference _factor;

        [Tooltip("The constant that will be added to the input."), SerializeField]
        private TVarReference _constant;

        [Tooltip("Where will the result of the linear function be written to?"), SerializeField]
        private TGlobalVar _result;

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

        public TVarReference Parameter
        {
            get { return _parameter; }
        }

        public TVarReference Factor
        {
            get { return _factor; }
        }

        public TVarReference Constant
        {
            get { return _constant; }
        }

        protected abstract TVar PerformLinearFunction();

        private void UpdateResult(object subject, VariableReferenceEvent args)
        {
            if (_result == null ||
                !_parameter.IsValid ||
                !_factor.IsValid ||
                !_constant.IsValid)
            {
                return;
            }

            _result.Value = PerformLinearFunction();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _parameter?.SubscribeToEvents(UpdateResult);
            _factor?.SubscribeToEvents(UpdateResult);
            _constant?.SubscribeToEvents(UpdateResult);
        }

        private void UnsubscribeFromEvents()
        {
            _parameter?.UnsubscribeFromEvents(UpdateResult);
            _factor?.UnsubscribeFromEvents(UpdateResult);
            _constant?.UnsubscribeFromEvents(UpdateResult);
        }
    }
}
