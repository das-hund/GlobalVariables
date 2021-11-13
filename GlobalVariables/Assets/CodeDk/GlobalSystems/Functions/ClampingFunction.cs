using System;
using UnityEngine;

namespace CodeDk
{
    public abstract class ClampingFunction : GlobalSystem
    {
        public event EventHandler<RangeBreachedEvent> ValueBreachedRange;

        [Tooltip("How will values outside of the range be mapped into the range?"), SerializeField]
        private RangeWrapMode _wrapMode = RangeWrapMode.Clamp;

        public RangeWrapMode WrapMode
        {
            get { return _wrapMode; }
        }

        protected virtual void OnValueBreachedRange(RangeBreachedEvent e)
        {
            ValueBreachedRange?.Invoke(this, e);
        }
    }

    public abstract class ClampingFunction<TVar, TGlobalVar, TVarReference> : ClampingFunction
        where TGlobalVar : GlobalVariable<TVar>
        where TVarReference : VariableReference<TVar, TGlobalVar>
    {
        [Tooltip("The variable that is to be clamped."), SerializeField]
        private TVarReference _parameter;

        [Tooltip("Minimum allowed value."), SerializeField]
        private TVarReference _minValue;

        [Tooltip("Maximum allowed value."), SerializeField]
        private TVarReference _maxValue;

        [Tooltip("Where will the result of the clamping be written to?"), SerializeField]
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

        public TVarReference MinValue
        {
            get { return _minValue; }
        }

        public TVarReference MaxValue
        {
            get { return _maxValue; }
        }

        public override void RunOnce()
        {
            if (_result == null ||
                !_parameter.IsValid ||
                !_minValue.IsValid ||
                !_maxValue.IsValid)
            {
                return;
            }

            var clampResult = PerformClamping();

            if (clampResult.DidBreachRange)
            {
                _result.Value = clampResult.Result;
                OnValueBreachedRange(RangeBreachedEvent.Empty);
            }
        }

        protected abstract (TVar Result, bool DidBreachRange) PerformClamping();

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

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _parameter?.SubscribeToEvents(UpdateResult);
            _minValue?.SubscribeToEvents(UpdateResult);
            _maxValue?.SubscribeToEvents(UpdateResult);
        }

        private void UnsubscribeFromEvents()
        {
            _parameter?.UnsubscribeFromEvents(UpdateResult);
            _minValue?.UnsubscribeFromEvents(UpdateResult);
            _maxValue?.UnsubscribeFromEvents(UpdateResult);
        }
    }
}
