using System;

namespace CodeDk
{
    public class VariableReferenceEvent : EventArgs
    {
        public static new readonly VariableReferenceEvent Empty = new VariableReferenceEvent(null);

        private readonly VariableReference _referenceSource;
        private readonly GlobalVariable _globalSource;
        private readonly GlobalVariableEvent _forwardedEventArgs;

        public VariableReferenceEvent(VariableReference referenceParam)
        {
            _referenceSource = referenceParam;
            _globalSource = null;
            _forwardedEventArgs = null;
        }

        public VariableReferenceEvent(VariableReference referenceParam, GlobalVariable globalParam, GlobalVariableEvent forwardedArgsParam)
        {
            _referenceSource = referenceParam;
            _globalSource = globalParam;
            _forwardedEventArgs = forwardedArgsParam;
        }

        public bool IsForwarded
        {
            get { return _globalSource != null; }
        }

        public VariableReference ReferenceSource
        {
            get { return _referenceSource; }
        }

        public GlobalVariable GlobalSource
        {
            get { return _globalSource; }
        }

        public GlobalVariableEvent ForwardedEventArgs
        {
            get { return _forwardedEventArgs; }
        }
    }
}
