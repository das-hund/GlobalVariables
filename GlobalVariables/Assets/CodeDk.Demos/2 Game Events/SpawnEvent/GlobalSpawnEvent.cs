using System;
using CodeDk;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Spawn Event", order = 251)]
public class GlobalSpawnEvent : GlobalGameEvent<SpawnEvent, SpawnEventArgs>
{
}