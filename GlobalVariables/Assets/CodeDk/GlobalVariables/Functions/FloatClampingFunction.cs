using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Clamping Function", menuName = "Global Variables/Functions/Clamping (Float)")]
    public class FloatClampingFunction : ClampingFunction<float, FloatVariable, FloatReference>
    {
        protected override (float Result, bool DidBreachRange) PerformClamping()
        {
            return WrapMode switch
            {
                RangeWrapMode.None => (Parameter, false),
                RangeWrapMode.Clamp => FloatClamping.Clamp(Parameter, MinValue, MaxValue),
                RangeWrapMode.Repeat => FloatClamping.Repeat(Parameter, MinValue, MaxValue),
                RangeWrapMode.PingPong => FloatClamping.PingPong(Parameter, MinValue, MaxValue),
                _ => (Parameter, false),
            };
        }
    }
}
