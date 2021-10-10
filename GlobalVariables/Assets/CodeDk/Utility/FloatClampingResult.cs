namespace CodeDk
{
    public struct FloatClampingResult
    {
        private readonly bool _didBreachRange;
        private readonly float _result;

        public FloatClampingResult(bool didBreachRangeParam, float resultParam)
        {
            _didBreachRange = didBreachRangeParam;
            _result = resultParam;
        }

        public bool DidBreachRange
        {
            get { return _didBreachRange; }
        }

        public float Result
        {
            get { return _result; }
        }

        public override string ToString()
        {
            return string.Format("FloatClampingResult(didBreachRange={0}, result={1})", _didBreachRange, _result);
        }
    }
}
