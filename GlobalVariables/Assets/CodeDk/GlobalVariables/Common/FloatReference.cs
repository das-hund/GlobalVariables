using System;

namespace CodeDk
{
    [Serializable]
    public class FloatReference : VariableReference<float, FloatVariable>
    {
        public FloatReference(float initValue) : base(initValue)
        { }
    }
}