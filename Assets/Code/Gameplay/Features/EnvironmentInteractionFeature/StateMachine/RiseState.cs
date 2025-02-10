using UnityEngine;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class RiseState : EnvironmentInteractionState
    {
        public RiseState(EnvironmentInteractionContext context,
            EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate) : base(context, estate)
        { }

        public override void EnterState(){}
        public override void ExitState(){}
        public override void UpdateState(){}

        public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
        {
            return StateKey;
        }
        public override void OnTriggerEnter(Collider other){}
        public override void OnTriggerStay(Collider other){}
        public override void OnTriggerExit(Collider other){}
    }
}