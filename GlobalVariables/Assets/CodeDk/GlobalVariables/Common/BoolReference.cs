using System;

namespace CodeDk
{
    [Serializable]
    public class BoolReference : VariableReference<bool, BoolVariable>
    {
        public BoolReference(bool initValue) : base(initValue)
        { }
    }
}
