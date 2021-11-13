using System;
using UnityEngine;

#if UNITY_EDTIOR
using UnityEditor;
#endif

namespace CodeDk
{
    [Serializable]
    public abstract class GlobalVariable : ScriptableObject
    {
        public event EventHandler<GlobalVariableEvent> ChangedEvent;

        public void RaiseChangedEvent()
        {
            if (ChangedEvent == null)
                return;

            ChangedEvent.Invoke(this, GlobalVariableEvent.Empty);
        }

        public virtual void SubscribeToEvents(EventHandler<GlobalVariableEvent> handler)
        {
            ChangedEvent += handler;
        }

        public virtual void UnsubscribeFromEvents(EventHandler<GlobalVariableEvent> handler)
        {
            ChangedEvent -= handler;
        }

        public abstract object UntypedValue
        { get; }
    }

    [Serializable]
    public abstract class GlobalVariable<T> : GlobalVariable
    {
        [SerializeField]
        private T _value;

        public override object UntypedValue
        {
            get { return _value; }
        }

        public virtual T Value
        {
            get { return _value; }
            set
            {
                if (value.Equals(_value))
                    return;

                _value = value;
                RaiseChangedEvent();
            }
        }

        public static implicit operator T(GlobalVariable<T> variableOfT)
        {
            return variableOfT.Value;
        }

        public T CopyVariable()
        {
            if (typeof(T).IsValueType)
            {
                return Value;
            }
            else
            {
                // Calling Instantiate ensures we get an actual copy of the object, rather than a 
                // reference. Unless T is a UnityEngine.Object, in which case it is also just a 
                // reference.
                // This creates and destroys a ScriptableObject.
                var globalCopy = Instantiate(this);
                T variableCopy = globalCopy.Value;
                DestroyImmediate(globalCopy);

                return variableCopy;
            }
        }

        #region Editor Only

#if UNITY_EDITOR && UNITY_2017_2_OR_NEWER

        [Header("Editor Only Settings"), Space]

        [Tooltip("Do not save changes made during play mode. Note: This is the default behavior for builds.")]
        [SerializeField]
        private bool _readOnly = true;

        [Tooltip("Use this area to add helpful developer notes.")]
        [SerializeField, Multiline]
        private string _description;

        private T _originalValue;

        private bool _backupCreated = false;

        public void OnEnable()
        {
            SubscribeToPlaymodeChanges();

            // We might have missed the Playmode change where we want to create the backup,
            // so ensure that we have one when already playing.  
            if (Application.isPlaying && UnityEditor.EditorUtility.IsPersistent(this))
            {
                EnsureBackupExists();
            }
        }

        public void OnDisable()
        {
            UnsubscribeToPlaymodeChanges();
        }

        private void EnsureBackupExists()
        {
            if (!_backupCreated)
            {
                _originalValue = _value;
                _value = CopyVariable();
                _backupCreated = true;
            }
        }

        private void SubscribeToPlaymodeChanges()
        {
            UnsubscribeToPlaymodeChanges();
            UnityEditor.EditorApplication.playModeStateChanged += OnPlaymodeChange;
        }

        private void UnsubscribeToPlaymodeChanges()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlaymodeChange;
        }

        public void OnPlaymodeChange(UnityEditor.PlayModeStateChange stateChange)
        {
            if (stateChange == UnityEditor.PlayModeStateChange.ExitingEditMode)
            {
                // We definitely want to create a new backup before entering playmode
                _backupCreated = false;
                EnsureBackupExists();
            }
            else if (stateChange == UnityEditor.PlayModeStateChange.EnteredEditMode)
            {
                // Invalidate the backup, so in case we miss the playmode change where
                // we enter playmode, we know to create a new backup in OnEnable
                _backupCreated = false;

                // We are entering edit mode, restore the original value, if readonly is set
                if (_readOnly)
                {
                    _value = _originalValue;
                }
                else
                {
                    UnityEditor.EditorUtility.SetDirty(this);
                }
            }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

#endif

        #endregion
    }
}
