using UnityEngine;

namespace Code.Gameplay.Movement.Controller.StateMachine
{
    public class CCStandState: CharacterControllerState
    {
        
        public CCStandState(CharacterControllerContext context,
            CharacterControllerStateMachine.ECharacterControllerStates stateKey)
            : base(context,stateKey)
        {
            Context = context;
        }
        public override void EnterState()
        {
        }

        public override void ExitState()
        {   
        }

        public override void UpdateState()
        {
        }

        public override CharacterControllerStateMachine.ECharacterControllerStates GetNextState()
        {
            return StateKey;
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerStay(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}