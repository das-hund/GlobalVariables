using System;
using UnityEngine;

namespace CodeDk
{
    public abstract class GlobalSystem : ScriptableObject
    {
        public event EventHandler<EventArgs> EnabledChanged;

        public abstract void RunOnce();

        [SerializeField]
        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled == value)
                {
                    return;
                }

                _enabled = value;
                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
