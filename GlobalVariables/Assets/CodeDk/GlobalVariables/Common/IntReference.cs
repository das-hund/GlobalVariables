using System;

namespace CodeDk
{
    [Serializable]
    public class IntReference : VariableReference<int, IntVariable>
    {
        public IntReference(int initValue) : base(initValue)
        { }
    }
}