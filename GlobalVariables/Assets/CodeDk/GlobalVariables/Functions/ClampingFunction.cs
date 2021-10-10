using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Clamping Function", menuName = "Global Variables/Functions/Clamping")]
    public class ClampingFunction : ScriptableObject
    {
        [Tooltip("The variable that is to be clamped."), SerializeField]
        private FloatReference _parameter;

        [Tooltip("Minimum allowed value."), SerializeField]
        private FloatReference _minValue;

        [Tooltip("Maximum allowed value."), SerializeField]
        private FloatReference _maxValue;

        [Tooltip("Where will the result of the clamping be written to?"), SerializeField]
        private FloatVariable _result;

        [Tooltip("How will values outside of the range be mapped into the range?"), SerializeField]
        private RangeWrapMode _wrapMode = RangeWrapMode.Clamp;

        public event EventHandler<RangeBreachedEvent> ValueBreachedRange;

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

        public FloatReference MinValue
        {
            get { return _minValue; }
        }

        public FloatReference MaxValue
        {
            get { return _maxValue; }
        }

        public RangeWrapMode WrapMode
        {
            get { return _wrapMode; }
        }

        private void UpdateResult(object subject, VariableReferenceEvent args)
        {
            if (_result == null ||
                !_parameter.IsValid ||
                !_minValue.IsValid ||
                !_maxValue.IsValid)
            {
                return;
            }

            PerformClamping();
        }

        private void PerformClamping()
        {
            var clampingResult = _wrapMode switch
            {
                RangeWrapMode.None => new FloatClampingResult(false, _parameter.Value),
                RangeWrapMode.Clamp => FloatClamping.Clamp(_parameter.Value, MinValue, MaxValue),
                RangeWrapMode.Repeat => FloatClamping.Repeat(_parameter.Value, MinValue, MaxValue),
                RangeWrapMode.PingPong => FloatClamping.PingPong(_parameter.Value, MinValue, MaxValue),
                _ => new FloatClampingResult(false, _parameter.Value),
            };

            if (clampingResult.DidBreachRange)
            {
                _result.Value = clampingResult.Result;
                ValueBreachedRange?.Invoke(this, RangeBreachedEvent.Empty);
            }
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
