using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.StateMachine
{
    public abstract class StateManager<EState> : MonoBehaviour, IStateManager where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new();
        protected BaseState<EState> CurrentState;

        protected bool IsTransitioningToState = false;

        // private void Start() // MonoBehStuff
        // {
        //     CurrentState.EnterState();
        // }
        //
        // private void Update()
        // {
        //     EState nextStateKey = CurrentState.GetNextState();
        //
        //     if (!IsTransitioningToState && nextStateKey.Equals(CurrentState.StateKey))
        //         CurrentState.UpdateState();
        //     else if(!IsTransitioningToState)
        //         TransitionToState(nextStateKey);
        // }
        public virtual void InitializeStateMachine(){}
        public virtual void StartStateMachine()
        {
            CurrentState.EnterState();
        }
        public virtual void Tick()
        {
            EState nextStateKey = CurrentState.GetNextState();

            if (!IsTransitioningToState && nextStateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else if(!IsTransitioningToState)
                TransitionToState(nextStateKey);
            
            Debug.Log("CurrentState is " + CurrentState.StateKey);
        }
        public void TransitionToState(EState key)
        {
            IsTransitioningToState = true;
            CurrentState.ExitState();
            CurrentState = States[key];
            CurrentState.EnterState();
            IsTransitioningToState = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnter(other);
        }
        private void OnTriggerStay(Collider other) 
        { 
            CurrentState.OnTriggerStay(other);
        }
        private void OnTriggerExit(Collider other) 
        {
            CurrentState.OnTriggerExit(other);
        }
        private void OnCollisionEnter(Collision other) { }
        private void OnCollisionStay(Collision other) { }
        private void OnCollisionExit(Collision other) { }
    }
}