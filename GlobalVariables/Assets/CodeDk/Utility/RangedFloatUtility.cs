using UnityEngine;

namespace CodeDk
{
    public static class FloatClamping
    {
        public static FloatClampingResult Clamp(float value, float minValue, float maxValue)
        {
            Debug.AssertFormat(minValue <= maxValue,
                $"Parameter minValue (minValue) must be less than or equal to parameter maxValue ({maxValue})!");

            if (value < minValue)
            {
                return new FloatClampingResult(true, minValue);
            }
            else if (value > maxValue)
            {
                return new FloatClampingResult(true, maxValue);
            }
            else
            {
                return new FloatClampingResult(false, value);
            }
        }

        public static FloatClampingResult Repeat(float value, float minValue, float maxValue)
        {
            Debug.AssertFormat(minValue <= maxValue,
                $"Parameter minValue (minValue) must be less than or equal to parameter maxValue ({maxValue})!");

            if (value >= minValue && value <= maxValue)
                return new FloatClampingResult(false, value);

            float rangeLength = maxValue - minValue;
            float valueShiftedToRange = value - minValue;

            float newValue = Mathf.Repeat(valueShiftedToRange, rangeLength);

            value = minValue + newValue;

            return new FloatClampingResult(true, value);
        }

        public static FloatClampingResult PingPong(float value, float minValue, float maxValue)
        {
            Debug.AssertFormat(minValue <= maxValue,
                "Parameter minValue ({0}) must be less than or equal to parameter maxValue ({0})!",
                minValue, maxValue);

            if (value >= minValue && value <= maxValue)
                return new FloatClampingResult(false, value);

            float rangeLength = maxValue - minValue;
            float valueShiftedToRange = value - minValue;

            float newValue = Mathf.PingPong(valueShiftedToRange, rangeLength);

            value = minValue + newValue;

            return new FloatClampingResult(true, value);
        }
    }
}


