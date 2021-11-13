using System;
using UnityEngine;

namespace CodeDk
{
    public class GlobalGameEventListener<EventArgType, GameEventType, GlobalEventType, UnityEventType> : MonoBehaviour
        where EventArgType : EventArgs
        where GameEventType : GameEvent<EventArgType>
        where GlobalEventType : GlobalGameEvent<GameEventType, EventArgType>
        where UnityEventType : UnityGameEvent<EventArgType>
    {
        public GlobalEventType globalEvent;
        public UnityEventType response;

        public void OnEnable()
        {
            Subscribe();
        }

        public void OnDisable()
        {
            Unsubscribe();
        }

        public void Subscribe()
        {
            globalEvent.Subscribe(RaiseResponse);
        }

        public void Unsubscribe()
        {
            globalEvent.Unsubscribe(RaiseResponse);
        }

        public virtual void RaiseResponse(object sender, EventArgType parameter)
        {
            response.Invoke(sender, parameter);
        }
    }
}
