using UnityEngine;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class EnvIntTouchState : EnvironmentInteractionState
    {
        public float _elapsedTime = 0.0f;
        public float _resetThreshold = 5.5f;
        
        public EnvIntTouchState(EnvironmentInteractionContext context,
            EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate) : base(context, estate)
        {
        }

        public override void EnterState()
        {
            _elapsedTime = 0.0f;
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
        }

        public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
        {
            if(_elapsedTime > _resetThreshold || CheckShouldReset() || Context.CurrentIntersectingCollider == null)
                return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Reset;
            
            return StateKey;
        }

        public override void OnTriggerEnter(Collider other)
        {
            StartIKTargetPositionTracking(other);
        }

        public override void OnTriggerStay(Collider other)
        {
            UpdateIKTargetPosition(other);
        }

        public override void OnTriggerExit(Collider other)
        {
            ResetIKTargetPositionTracking(other);
        }
    }
}