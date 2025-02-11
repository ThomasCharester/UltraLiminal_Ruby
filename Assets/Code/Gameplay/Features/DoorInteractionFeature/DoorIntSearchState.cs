using UnityEngine;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public class DoorIntSearchState : DoorInteractionState
    {
        public float _approachDistanceThreshold = 2.0f;
        private float _elapsedLeftTime = 0.0f;
        private float _elapsedRightTime = 0.0f;
        private float _elapsedBothTime = 0.0f;

        public DoorIntSearchState(DoorInteractionContext context,
            DoorInteractionStateMachine.EDoorInteractionState estate) : base(context, estate)
        {
        }

        public override void EnterState()
        {
            _elapsedRightTime = 0.0f;
            _elapsedLeftTime = 0.0f;
            _elapsedBothTime = 0.0f;
            
            Context.ClosestPointOnColliderFromShoulder = Vector3.positiveInfinity;
            Context.CurrentIntersectingCollider = null;
        }

        public override void ExitState()
        {   
        }

        public override void UpdateState()
        {
            if (Context.CurrentIntersectingCollider == null)
            {
                _elapsedRightTime += Time.deltaTime;
                _elapsedLeftTime += Time.deltaTime;
                _elapsedBothTime += Time.deltaTime;
                
                Context.ResetHands(_elapsedBothTime);
            }
            else if (Context.CurrentBodySide == DoorInteractionContext.EBodySide.Right)
            {
                _elapsedBothTime = 0.0f;
                
                _elapsedRightTime = 0.0f;
                _elapsedLeftTime += Time.deltaTime;
                Context.ResetHand(DoorInteractionContext.EBodySide.Left,_elapsedLeftTime);
            }
            else 
            {
                _elapsedBothTime = 0.0f;
                
                _elapsedLeftTime = 0.0f;
                _elapsedRightTime += Time.deltaTime;
                Context.ResetHand(DoorInteractionContext.EBodySide.Right,_elapsedRightTime);
            }
            
        }

        public override DoorInteractionStateMachine.EDoorInteractionState GetNextState()
        {
            if (CheckShouldReset())
                return StateKey;
    
            bool isCloseToTarget = Vector3.Distance(Context.ClosestPointOnColliderFromShoulder, Context.RootTransform.position) <
                                   _approachDistanceThreshold;
            bool isClosestPointOnColliderValid = Context.ClosestPointOnColliderFromShoulder != Vector3.positiveInfinity;
            
            if (isCloseToTarget && isClosestPointOnColliderValid)
                return DoorInteractionStateMachine.EDoorInteractionState.Rise;
            
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