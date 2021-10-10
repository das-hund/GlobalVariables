using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Linear Function", menuName = "Global Variables/Functions/Linear (Float)")]
    public class FloatLinearFunction : LinearFunction<float, FloatVariable, FloatReference>
    {
        protected override float PerformLinearFunction()
        {
            return Parameter * Factor + Constant;
        }
    }
}
