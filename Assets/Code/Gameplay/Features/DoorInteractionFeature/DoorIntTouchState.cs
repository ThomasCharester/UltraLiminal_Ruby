using UnityEngine;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public class DoorIntTouchState: DoorInteractionState
    {
        public float _elapsedTime = 0.0f;
        public float _resetThreshold = 5.5f;
        
        public DoorIntTouchState(DoorInteractionContext context,
            DoorInteractionStateMachine.EDoorInteractionState estate) : base(context, estate)
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

        public override DoorInteractionStateMachine.EDoorInteractionState GetNextState()
        {
            if(/*_elapsedTime > _resetThreshold || */CheckShouldReset() || Context.CurrentIntersectingCollider == null)
                return DoorInteractionStateMachine.EDoorInteractionState.Reset;
            
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