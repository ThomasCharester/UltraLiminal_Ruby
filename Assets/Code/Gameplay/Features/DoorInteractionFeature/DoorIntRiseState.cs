using Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine;
using UnityEngine;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public class DoorIntRiseState: DoorInteractionState
    {
        private float _elapsedTime = 0.0f;
        private float _lerpDuration = 5.0f;
        private float _riseWeight = 1.0f;
        private Quaternion _expectedHandRotation;
        private float _maxDistance = .5f;
        protected LayerMask _interactableLayerMask = LayerMask.GetMask("Interactable");
        private float _rotationSpeed = 1000f;
        private float _touchDistanceThreshold = .5f;
        private float _touchTimeThreshold = 1f;
        
        public DoorIntRiseState(DoorInteractionContext context,
            DoorInteractionStateMachine.EDoorInteractionState estate) : base(context, estate)
        { }

        public override void EnterState()
        {
            _elapsedTime = 0.0f;
        }
        public override void ExitState(){}

        public override void UpdateState()
        {
            CalculateExpectedHandRotation();
            
            //Context.InteractionPointYOffset = Mathf.Lerp(Context.InteractionPointYOffset, Context.ClosestPointOnColliderFromShoulder.y, _elapsedTime / _lerpDuration);
            
            Context.CurrentIKConstraint.weight = Mathf.Lerp(Context.CurrentIKConstraint.weight, _riseWeight, _elapsedTime / _lerpDuration);
            
            Context.CurrentMultiRotationConstraint.weight = Mathf.Lerp(Context.CurrentMultiRotationConstraint.weight, _riseWeight, _elapsedTime / _lerpDuration);
            
            Context.CurrentIKTargetTransform.rotation = Quaternion.RotateTowards(Context.CurrentIKTargetTransform.rotation, _expectedHandRotation,
                _rotationSpeed * Time.deltaTime);
            
            _elapsedTime += Time.deltaTime;
        }

        private void CalculateExpectedHandRotation()
        {
            Vector3 startPos = Context.CurrentShoulderTransform.position;
            Vector3 endPos = Context.ClosestPointOnColliderFromShoulder;
            Vector3 direction = (endPos - startPos).normalized;

            RaycastHit hit;

            if (Physics.Raycast(startPos, direction, out hit, _maxDistance, _interactableLayerMask))
            {
                Vector3 surfaceNormal = hit.normal;
                Vector3 targetForward = -surfaceNormal;
                
                _expectedHandRotation = Quaternion.LookRotation(targetForward, Vector3.up);
            }
        }

        public override DoorInteractionStateMachine.EDoorInteractionState GetNextState()
        {
            if (CheckShouldReset())
                 return DoorInteractionStateMachine.EDoorInteractionState.Reset;
            
            
            if(Vector3.Distance(Context.CurrentIKTargetTransform.position, Context.ClosestPointOnColliderFromShoulder) < _touchDistanceThreshold 
               && _elapsedTime >= _touchTimeThreshold)
                return DoorInteractionStateMachine.EDoorInteractionState.Touch;
            
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