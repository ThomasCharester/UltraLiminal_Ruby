using NUnit.Framework.Internal.Filters;
using UnityEngine;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class ResetState : EnvironmentInteractionState
    {
        private float _elapsedTime = 0.0f;
        private float _resetDuration = 2.0f;
        private float _lerpDuration = 10.0f;
        private float _rotationSpeed = 500.0f;

        public ResetState(EnvironmentInteractionContext context,
            EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate) : base(context, estate)
        { }

        public override void EnterState()
        {
            _elapsedTime = 0.0f;
            Context.ClosestPointOnColliderFromShoulder = Vector3.positiveInfinity;
            Context.CurrentIntersectingCollider = null;
        }
        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            _elapsedTime += Time.deltaTime;
            Context.InteractionPointYOffset = Mathf.Lerp(Context.InteractionPointYOffset, Context.ColliderCenterY, _elapsedTime / _lerpDuration);
            
            Context.CurrentMultiRotationConstraint.weight = Mathf.Lerp(Context.CurrentMultiRotationConstraint.weight, 0, _elapsedTime / _lerpDuration);
            Context.CurrentIKConstraint.weight = Mathf.Lerp(Context.CurrentIKConstraint.weight, 0, _elapsedTime / _lerpDuration);
            
            Context.CurrentIKTargetTransform.localPosition = Vector3.Lerp(Context.CurrentIKTargetTransform.localPosition,
                Context.CurrentOriginalTargetPosition, _elapsedTime / _lerpDuration);
            Context.CurrentIKTargetTransform.rotation = Quaternion.RotateTowards(Context.CurrentIKTargetTransform.rotation,
                Context.OriginalTargetRotation,_rotationSpeed * Time.deltaTime);
        }

        public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
        {
            if (_elapsedTime >= _resetDuration)
            {
                return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Search;
            }

            return StateKey;
        }
        public override void OnTriggerEnter(Collider other){}
        public override void OnTriggerStay(Collider other){}
        public override void OnTriggerExit(Collider other){}
    }
}