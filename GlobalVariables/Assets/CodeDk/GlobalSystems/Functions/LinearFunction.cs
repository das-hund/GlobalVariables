using System;
using UnityEngine;

namespace CodeDk
{
    public abstract class LinearFunction : GlobalSystem
    {
    }

    public abstract class LinearFunction<TVar, TGlobalVar, TVarReference> : LinearFunction
        where TGlobalVar : GlobalVariable<TVar>
        where TVarReference : VariableReference<TVar, TGlobalVar>
    {
        [Tooltip("The input variable."), SerializeField]
        private TVarReference _parameter;

        [Tooltip("The factor the input will be multiplied with."), SerializeField]
        private FloatReference _factor;

        [Tooltip("The constant that will be added to the input."), SerializeField]
        private TVarReference _constant;

        [Tooltip("Where will the result of the linear function be written to?"), SerializeField]
        private TGlobalVar _result;

        public void OnEnable()
        {
            EnabledChanged += OnEnabledChanged;
            OnEnabledChanged(this, EventArgs.Empty);
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

        public FloatReference Factor
        {
            get { return _factor; }
        }

        public TVarReference Constant
        {
            get { return _constant; }
        }

        public override void RunOnce()
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

        protected abstract TVar PerformLinearFunction();

        private void UpdateResult(object subject, VariableReferenceEvent args)
        {
            // If the event source is also the result sink, then one of the parameters
            // must also be the result sink. To avoid infinite loops, exit now!
            if (ReferenceEquals(_result, subject))
            {
                return;
            }

            RunOnce();
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
            {
                SubscribeToEvents();
            }
            else
            {
                UnsubscribeFromEvents();
            }
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
