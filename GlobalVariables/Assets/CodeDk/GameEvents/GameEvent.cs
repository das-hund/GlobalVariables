using System;

namespace CodeDk
{
    public abstract class GameEvent
    {
        public abstract void RaiseEvent();
    }

    public class GameEvent<EventArgType> : GameEvent
        where EventArgType : EventArgs
    {
        public event EventHandler<EventArgType> GameEventOccurred;

#if UNITY_EDITOR
        public EventArgType _inspectorArgs;
#endif

        public override void RaiseEvent()
        {
            if (GameEventOccurred != null)
#if UNITY_EDITOR
                GameEventOccurred.Invoke(this, _inspectorArgs);
#else
                GameEventOccurred.Invoke(this, default);
#endif
        }

        public void RaiseEvent(EventArgType args)
        {
            if (GameEventOccurred != null)
                GameEventOccurred.Invoke(this, args);
        }

        public void Subscribe(EventHandler<EventArgType> subscriber)
        {
            Unsubscribe(subscriber);
            GameEventOccurred += subscriber;
        }

        public void Unsubscribe(EventHandler<EventArgType> subscriber)
        {
            GameEventOccurred -= subscriber;
        }
    }
}
