using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable]
    public abstract class VariableReference
    {
        private event EventHandler<VariableReferenceEvent> ChangedEvent;

        [SerializeField]
        private bool _usingGlobal = true;

        public virtual void SubscribeToEvents(EventHandler<VariableReferenceEvent> handler)
        {
            ChangedEvent += handler;
        }

        public virtual void UnsubscribeFromEvents(EventHandler<VariableReferenceEvent> handler)
        {
            ChangedEvent -= handler;
        }

        public void RaiseChangeEvent()
        {
            if (ChangedEvent == null)
            {
                return;
            }

            ChangedEvent.Invoke(this, VariableReferenceEvent.Empty);
        }

        /// <summary>
        /// Used soley internally to forward the ChangedEvent of the referenced GlobalVariable to listeners
        /// of this VariableReference.
        /// 
        /// By default, only empty EventArgs are passed around to avoid memory allocation. Override this method if
        /// you need to pass around arguments.
        /// </summary>
        /// <param name="eventArgs">The event arguments to forward.</param>
        protected virtual void ForwardGlobalVariableEvent(object source, GlobalVariableEvent eventArgs)
        {
            if (ChangedEvent == null || eventArgs != GlobalVariableEvent.Empty)
            {
                return;
            }

            ChangedEvent.Invoke(this, VariableReferenceEvent.Empty);
        }

        public void Init()
        {
            SubscribeToGlobal();
        }

        public void OnBecameLocal()
        {
            UnsubscribeFromGlobal();
            RaiseChangeEvent();
        }

        public void OnBecameGlobal()
        {
            SubscribeToGlobal();
            RaiseChangeEvent();
        }

        protected void SubscribeToGlobal()
        {
            if (!IsGlobal || !IsValid)
            {
                return;
            }

            UnsubscribeFromGlobal();

            UntypedGlobalVariable.SubscribeToEvents(ForwardGlobalVariableEvent);
        }

        protected void UnsubscribeFromGlobal()
        {
            if (!IsGlobal || !IsValid)
            {
                return;
            }

            UntypedGlobalVariable.UnsubscribeFromEvents(ForwardGlobalVariableEvent);
        }

        public bool IsGlobal
        {
            get
            { return _usingGlobal; }
            set
            {
                if (_usingGlobal != value)
                {
                    _usingGlobal = value;
                    RaiseChangeEvent();
                }
            }
        }

        public bool IsLocal
        {
            get
            {
                return !_usingGlobal;
            }
            set
            {
                if (_usingGlobal != value)
                {
                    _usingGlobal = value;
                    RaiseChangeEvent();
                }
            }
        }

        public abstract GlobalVariable UntypedGlobalVariable { get; }

        public abstract bool IsValid { get; }

        public void SubscribeTo(GlobalVariable variable)
        {
            variable.SubscribeToEvents(ForwardGlobalVariableEvent);
        }

        public void UnsubscribeFrom(GlobalVariable variable)
        {
            variable.UnsubscribeFromEvents(ForwardGlobalVariableEvent);
        }
    }

    [Serializable]
    public class VariableReference<VariableType, GlobalVariableType> : VariableReference, ISerializationCallbackReceiver
        where GlobalVariableType : GlobalVariable<VariableType>
    {
        [SerializeField]
        private GlobalVariableType _globalVariable;

        [SerializeField]
        private VariableType _localValue;

        public VariableReference(VariableType initValue)
        {
            _localValue = initValue;
            IsLocal = true;
        }

        public VariableReference(GlobalVariableType initVariable)
        {
            _globalVariable = initVariable;
            IsGlobal = true;
        }

        /// <summary>
        /// Since VariableReference is not a UnityEngine.Object type, we will not receive the OnEnable() event.
        /// By implementing ISeralizationCallbackReceiver, we can use OnAfterDeserialize() as an initialization 
        /// method where we subscribe to the referenced GlobalVariable (should there be one).
        /// </summary>
        public void OnAfterDeserialize()
        {
            SubscribeToGlobal();
        }

        public void OnBeforeSerialize()
        { }

        public virtual VariableType Value
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException("Cannot return value on VariableReference in global state when globalReference is null!");
                }

                return IsGlobal ? GlobalVariable : LocalValue;
            }
            set
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException("Cannot assign value to VariableReference in global state when globalReference is null!");
                }

                if (IsGlobal)
                {
                    GlobalVariable.Value = value;
                }
                else
                {
                    LocalValue = value;
                }
            }
        }

        public GlobalVariableType GlobalVariable
        {
            get
            {
                return _globalVariable;
            }
            set
            {
                if (value.Equals(_globalVariable))
                {
                    return;
                }

                // Unsubscribe from currently reference GlobalVariable
                UnsubscribeFromGlobal();

                // Set the reference to the new GlobalVariable and subscribe to it's ChangeEvents
                _globalVariable = value;
                SubscribeToGlobal();

                // Update all subscribers of this VariableReference
                RaiseChangeEvent();
            }
        }

        public VariableType LocalValue
        {
            get { return _localValue; }
            set
            {
                if (value.Equals(_localValue))
                {
                    return;
                }

                _localValue = value;
                RaiseChangeEvent();
            }
        }

        public override GlobalVariable UntypedGlobalVariable
        {
            get
            {
                return _globalVariable;
            }
        }

        /// <summary>
        /// IsValid checks if this VariableReference is in a valid state. Meaning, if it can return a meaningful
        /// value.
        /// 
        /// If the VariableReference is pointing to the globalVariable but globalVariable has not been set yet,
        /// we are in an invalid state. We cannot return null as a sentinel value, as we do not know the type of 
        /// VariableType (e.g. it could be an int).
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return IsLocal || _globalVariable != null;
            }
        }

        public static implicit operator VariableType(VariableReference<VariableType, GlobalVariableType> variableReference)
        {
            return variableReference.Value;
        }
    }
}
