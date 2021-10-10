using System;
using System.Linq;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Clamping Function", menuName = "Global Variables/Functions/Clamping (Vector3)")]
    public class Vector3ClampingFunction : ClampingFunction<Vector3, Vector3Variable, Vector3Reference>
    {
        public event EventHandler<RangeBreachedEvent> ValueBreachedRangeX;
        public event EventHandler<RangeBreachedEvent> ValueBreachedRangeY;
        public event EventHandler<RangeBreachedEvent> ValueBreachedRangeZ;

        protected override void OnValueBreachedRange(RangeBreachedEvent e)
        {
            base.OnValueBreachedRange(e);
        }

        protected override (Vector3 Result, bool DidBreachRange) PerformClamping()
        {
            var clampingResult = Vector3Clamping.ComponentWiseClamp(Parameter, MinValue, MaxValue, WrapMode);

            if(clampingResult.DidBreachRange[0])
                ValueBreachedRangeX?.Invoke(this, RangeBreachedEvent.Empty);

            if (clampingResult.DidBreachRange[1])
                ValueBreachedRangeY?.Invoke(this, RangeBreachedEvent.Empty);

            if (clampingResult.DidBreachRange[2])
                ValueBreachedRangeZ?.Invoke(this, RangeBreachedEvent.Empty);

            return (clampingResult.Result, clampingResult.DidBreachRange.Any());
        }
    }
}
