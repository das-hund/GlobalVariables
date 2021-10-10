using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable]
    public class Vector2Reference : VariableReference<Vector2, Vector2Variable>
    {
        public Vector2Reference(Vector2 initValue) : base(initValue)
        { }
    }
}
