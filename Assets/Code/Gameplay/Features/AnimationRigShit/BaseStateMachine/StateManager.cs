using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.AnimationRigShit.BaseStateMachine
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new();
        protected BaseState<EState> CurrentState;

        protected bool IsTransitioningToState = false;
        
        private void Awake() { }

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

        public void StateMachineStart()
        {
            CurrentState.EnterState();
        }
        public void StateMachineTick()
        {
            EState nextStateKey = CurrentState.GetNextState();

            if (!IsTransitioningToState && nextStateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else if(!IsTransitioningToState)
                TransitionToState(nextStateKey);
        }
        public void TransitionToState(EState key)
        {
            CurrentState.ExitState();
            CurrentState = States[key];
            CurrentState.EnterState();
        }
        private void OnTriggerEnter(Collider other) { }
        private void OnTriggerStay(Collider other) { }
        private void OnTriggerExit(Collider other) { }
        private void OnCollisionEnter(Collision other) { }
        private void OnCollisionStay(Collision other) { }
        private void OnCollisionExit(Collision other) { }
    }
}