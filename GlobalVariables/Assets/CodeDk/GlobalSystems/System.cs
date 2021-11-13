using System;
using UnityEngine;

namespace CodeDk
{
    public class System : ScriptableObject
    {
        [SerializeField]
        private bool _enabled;

        public event EventHandler<EventArgs> EnabledChanged;

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
