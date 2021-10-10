namespace CodeDk
{
    public class RangeBreachedEvent : GlobalVariableEvent
    {
        public static new readonly RangeBreachedEvent Empty = new RangeBreachedEvent(null);

        public RangeBreachedEvent(GlobalVariable variableParam)
            : base(variableParam)
        { }
    }
}
