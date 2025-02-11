using System;
using Code.Gameplay.StateMachine;
using UnityEngine;

namespace Code.Gameplay.Movement.Controller.StateMachine
{
    public abstract class CharacterControllerState : BaseState<CharacterControllerStateMachine.ECharacterControllerStates>
    {
        protected CharacterControllerContext Context;
        private bool _shouldReset;

        public CharacterControllerState(CharacterControllerContext context,
            CharacterControllerStateMachine.ECharacterControllerStates stateKey)
            : base(stateKey)
        {
            Context = context;
        }
    }
}