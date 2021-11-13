using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Game Loop System", menuName = "Global Systems/Game Loop System")]
    public class GameLoopSystem : GlobalSystem
    {
        public void Trigger()
        {
            RunOnce();
        }

        public override void RunOnce()
        {
            _system.RunOnce();
        }

        [SerializeField, SerializeReference]
        private GlobalSystem _system;

        [SerializeField]
        private GameLoopEventType _gameLoopEventType;

        public GameLoopEventType GameLoopEventType
        {
            get { return _gameLoopEventType; }
        }
    }
}
