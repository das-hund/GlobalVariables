using System;

namespace CodeDk
{
    [Serializable]
    public class StringReference : VariableReference<string, StringVariable>
    {
        public StringReference(string initValue) : base(initValue)
        { }
    }
}
