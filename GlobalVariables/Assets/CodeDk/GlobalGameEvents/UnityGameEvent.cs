using System;
using UnityEngine.Events;

namespace CodeDk
{
    public class UnityGameEvent<EventArgType> : UnityEvent<object, EventArgType>
        where EventArgType : EventArgs
    {
    }
}
