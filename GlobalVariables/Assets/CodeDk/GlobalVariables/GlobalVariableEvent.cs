using System;

namespace CodeDk
{
    [Serializable]
    public class GlobalVariableEvent : EventArgs
    {
        public static new readonly GlobalVariableEvent Empty = new GlobalVariableEvent(null);

        private readonly GlobalVariable _source;

        public GlobalVariableEvent(GlobalVariable sourceParam)
        {
            _source = sourceParam;
        }

        public GlobalVariable Source
        {
            get { return _source; }
        }
    }

}
