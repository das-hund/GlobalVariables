using System;

namespace CodeDk
{
    public class GlobalGameEvent<GameEventType, EventArgType> : GlobalVariable<GameEventType>
        where GameEventType : GameEvent<EventArgType>
        where EventArgType : EventArgs
    {
        public void Subscribe(EventHandler<EventArgType> subscriber)
        {
            Value.Subscribe(subscriber);
        }

        public void Unsubscribe(EventHandler<EventArgType> subscriber)
        {
            Value.Unsubscribe(subscriber);
        }

        public void RaiseEvent(EventArgType e)
        {
            Value.RaiseEvent(e);
        }

        public void RaiseEvent()
        {
            Value.RaiseEvent(default);
        }
    }
}
