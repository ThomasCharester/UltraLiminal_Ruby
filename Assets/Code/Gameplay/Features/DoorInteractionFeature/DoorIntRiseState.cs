using UnityEngine;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public class DoorIntRiseState: DoorInteractionState
    {
        private float _riseWeight = 1.0f;
        private Quaternion _expectedHandRotation;
        private float _maxDistance = .5f;
        protected LayerMask _interactableLayerMask = LayerMask.GetMask("Doors");
        
        
        private float _elapsedLeftTime = 0.0f;
        private float _elapsedRightTime = 0.0f;
        
        public DoorIntRiseState(DoorInteractionContext context,
            DoorInteractionStateMachine.EDoorInteractionState estate) : base(context, estate)
        { }

        public override void EnterState()
        {
            _elapsedRightTime = 0.0f;
            _elapsedLeftTime = 0.0f;
        }
        public override void ExitState(){}

        public override void UpdateState()
        {
            CalculateExpectedHandRotation();
            
            Context.CurrentIKTargetTransform.rotation = Quaternion.RotateTowards(Context.CurrentIKTargetTransform.rotation, _expectedHandRotation,
                Context.RotationSpeed * Time.deltaTime);
            
            if (Context.CurrentBodySide == DoorInteractionContext.EBodySide.Right)
            {
                if(_elapsedRightTime >= 0) _elapsedRightTime -= Time.deltaTime;
                
                _elapsedLeftTime += Time.deltaTime;
            
                Context.CurrentIKConstraint.weight = Mathf.Lerp(Context.CurrentIKConstraint.weight, _riseWeight, _elapsedLeftTime / Context.LerpDuration);
            
                Context.CurrentMultiRotationConstraint.weight = Mathf.Lerp(Context.CurrentMultiRotationConstraint.weight, _riseWeight, _elapsedLeftTime / Context.LerpDuration);
                
                Context.ResetHand(DoorInteractionContext.EBodySide.Left,_elapsedLeftTime);
            }
            else 
            {
                if(_elapsedLeftTime >= 0) _elapsedLeftTime -= Time.deltaTime;
                
                _elapsedRightTime += Time.deltaTime;
            
                Context.CurrentIKConstraint.weight = Mathf.Lerp(Context.CurrentIKConstraint.weight, _riseWeight, _elapsedRightTime / Context.LerpDuration);
                
                Context.CurrentMultiRotationConstraint.weight = Mathf.Lerp(Context.CurrentMultiRotationConstraint.weight, _riseWeight, _elapsedRightTime / Context.LerpDuration);
                
                Context.ResetHand(DoorInteractionContext.EBodySide.Right,_elapsedRightTime);
            }
        }

        private void CalculateExpectedHandRotation()
        {
            Vector3 startPos = Context.CurrentShoulderTransform.position;
            Vector3 endPos = Context.ClosestPointOnColliderFromShoulder;
            Vector3 direction = (endPos - startPos); //.normalized

            RaycastHit hit;

            if (Physics.Raycast(startPos, direction, out hit, _maxDistance, _interactableLayerMask))
            {
                DebugExtension.DebugArrow(startPos, direction, Color.red, 0.3f);
                Vector3 surfaceNormal = hit.normal;
                Vector3 targetForward = -surfaceNormal;
                
                _expectedHandRotation = Quaternion.LookRotation(targetForward, Vector3.up);
            }
        }

        public override DoorInteractionStateMachine.EDoorInteractionState GetNextState()
        {
            if (CheckShouldReset() || Context.CurrentIntersectingCollider == null)
                 return DoorInteractionStateMachine.EDoorInteractionState.Search;
            
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