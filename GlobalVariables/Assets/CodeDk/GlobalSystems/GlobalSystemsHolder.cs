using UnityEngine;

namespace CodeDk
{
    public class GlobalSystemsHolder : MonoBehaviour
    {
        [SerializeReference]
        private GlobalSystem[] _reactiveSystems;

        [SerializeReference]
        private GameLoopSystem[] _intervalSystems;

        public FloatVariable DeltaTime;
        public FloatVariable FixedDeltaTime;

        public void Update()
        {
            DeltaTime.SetAndRaiseEvent(Time.deltaTime);
            TriggerSystems(GameLoopEventType.Update);
        }

        public void FixedUpdate()
        {
            FixedDeltaTime.SetAndRaiseEvent(Time.fixedDeltaTime);
            TriggerSystems(GameLoopEventType.FixedUpdate);
        }

        public void TriggerSystems(GameLoopEventType type)
        {
            foreach (var system in _intervalSystems)
            {
                if (system.GameLoopEventType == type)
                {
                    system.Trigger();
                }
            }
        }
    }
}
