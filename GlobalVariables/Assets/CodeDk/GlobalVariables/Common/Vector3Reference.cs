using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable]
    public class Vector3Reference : VariableReference<Vector3, Vector3Variable>
    {
        public Vector3Reference(Vector3 initValue) : base(initValue)
        { }
    }
}
