using System;
using CodeDk;

public class RandomEventRaiser : VoidGameEventListener
{
    public GlobalMoveEvent eventToRaise;

    public IntReference maxMoverId;

    public int valueStart = 1;
    public int valueEnd = 10;

    /// <summary>
    /// We want to reuse one instance of eventArgs to avoid garbage collection.
    /// </summary>
    private readonly MoveEventArgs _eventArgs = new MoveEventArgs(0, 0);

    public void RaiseRandomMoveEvent(object sender, EventArgs args)
    {
        int randomFilterKey = UnityEngine.Random.Range(0, maxMoverId);
        int randomParameter = UnityEngine.Random.Range(valueStart, valueEnd);

        _eventArgs.MoverId = randomFilterKey;
        _eventArgs.Position = randomParameter;

        eventToRaise.RaiseEvent(_eventArgs);
    }
}
