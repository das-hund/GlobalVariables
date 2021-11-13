using UnityEngine;

namespace CodeDk
{
    public static class FloatClamping
    {
        public static (float result, bool didBreachRange) Clamp(float value, float minValue, float maxValue)
        {
            Debug.AssertFormat(minValue <= maxValue,
                $"Parameter minValue (minValue) must be less than or equal to parameter maxValue ({maxValue})!");

            if (value < minValue)
            {
                return (minValue, true);
            }
            else if (value > maxValue)
            {
                return (maxValue, true);
            }
            else
            {
                return (value, false);
            }
        }

        public static (float result, bool didBreachRange) Repeat(float value, float minValue, float maxValue)
        {
            Debug.AssertFormat(minValue <= maxValue,
                $"Parameter minValue (minValue) must be less than or equal to parameter maxValue ({maxValue})!");

            if (value >= minValue && value <= maxValue)
            {
                return (value, false);
            }

            float rangeLength = maxValue - minValue;
            float valueShiftedToRange = value - minValue;

            float newValue = Mathf.Repeat(valueShiftedToRange, rangeLength);

            value = minValue + newValue;

            return (value, true);
        }

        public static (float result, bool didBreachRange) PingPong(float value, float minValue, float maxValue)
        {
            Debug.AssertFormat(minValue <= maxValue,
                "Parameter minValue ({0}) must be less than or equal to parameter maxValue ({0})!",
                minValue, maxValue);

            if (value >= minValue && value <= maxValue)
            {
                return (value, false);
            }

            float rangeLength = maxValue - minValue;
            float valueShiftedToRange = value - minValue;

            float newValue = Mathf.PingPong(valueShiftedToRange, rangeLength);

            value = minValue + newValue;

            return (value, true);
        }
    }
}


