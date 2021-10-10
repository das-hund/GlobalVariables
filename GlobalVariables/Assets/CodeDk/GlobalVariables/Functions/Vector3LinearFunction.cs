using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Linear Function", menuName = "Global Variables/Functions/Linear (Vector3)")]
    public class Vector3LinearFunction : LinearFunction<Vector3, Vector3Variable, Vector3Reference>
    {
        protected override Vector3 PerformLinearFunction()
        {
            return Vector3.Scale(Parameter, Factor) + Constant;
        }
    }
}
