using System;
using CodeDk;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Move Event", order = 251)]
public class GlobalMoveEvent : GlobalGameEvent<MoveEvent, MoveEventArgs>
{
}
