using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Game Event", order = 250)]
    public class GlobalVoidGameEvent : GlobalGameEvent<VoidGameEvent, EventArgs>
    { }
}
