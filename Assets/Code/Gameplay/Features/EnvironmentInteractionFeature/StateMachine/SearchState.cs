using UnityEngine;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class SearchState : EnvironmentInteractionState
    {
        public float _approachDistanceThreshold = 2.0f;
        private float _elapsedTime = 0.0f;
        private float _lerpDuration = 10.0f;
        private float _rotationSpeed = 500.0f;

        public SearchState(EnvironmentInteractionContext context,
            EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate) : base(context, estate)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {   
        }

        public override void UpdateState()
        {
            if (Context.CurrentIntersectingCollider == null)
            {
                _elapsedTime += Time.deltaTime;
                Context.CurrentIKTargetTransform.localPosition = Vector3.Lerp(
                    Context.CurrentIKTargetTransform.localPosition,
                    Context.CurrentOriginalTargetPosition, _elapsedTime / _lerpDuration);
                Context.CurrentIKTargetTransform.rotation = Quaternion.RotateTowards(
                    Context.CurrentIKTargetTransform.rotation,
                    Context.OriginalTargetRotation, _rotationSpeed * Time.deltaTime);
            }
            else
                _elapsedTime = 0;
            
        }

        public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
        {

            bool isCloseToTarget =
                Vector3.Distance(Context.ClosestPointOnColliderFromShoulder, Context.RootTransform.position) <
                _approachDistanceThreshold;
            bool isClosestPointOnColliderValid = Context.ClosestPointOnColliderFromShoulder != Vector3.positiveInfinity;


            if (isCloseToTarget && isClosestPointOnColliderValid)
                return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Approach;
            
            // if (CheckShouldReset())
            //      return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Reset;
            
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