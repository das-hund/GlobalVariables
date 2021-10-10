using UnityEngine;

namespace CodeDk
{
    public static class Vector3Clamping
    {
        public static (Vector3 Result, bool[] DidBreachRange) ComponentWiseClamp(Vector3 input, Vector3 min, Vector3 max, RangeWrapMode mode)
        {
            (Vector3 Result, bool[] DidBreachRange) clampResult = (Vector3.zero, new bool[]{false, false, false});

            for (int i = 0; i < 3; i++)
            {
                Debug.AssertFormat(min[i] <= max[i],
                    $"Parameter minValue (min[i]) must be less than or equal to parameter maxValue ({max[i]}) (in component {i})!");

                (float Result, bool DidBreachRange) componentClampResult = mode switch
                {
                    RangeWrapMode.None => (input[i], false),
                    RangeWrapMode.Clamp => FloatClamping.Clamp(input[i], min[i], max[i]),
                    RangeWrapMode.Repeat => FloatClamping.Repeat(input[i], min[i], max[i]),
                    RangeWrapMode.PingPong => FloatClamping.PingPong(input[i], min[i], max[i]),
                    _ => (input[i], false)
                };

                clampResult.Result[i] = componentClampResult.Result;
                clampResult.DidBreachRange[i] = componentClampResult.DidBreachRange;
            }

            return clampResult;
        }
    }
}
