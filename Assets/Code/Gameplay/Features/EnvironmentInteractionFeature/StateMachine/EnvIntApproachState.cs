using UnityEngine;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class EnvIntApproachState : EnvironmentInteractionState
    {
        private float _elapsedTime = 0.0f;
        private float _lerpDuration = 5.0f;
        private float _approachDuration = 2.0f;
        private float _approachWeight = .5f;
        private float _approachRotationWeight = .75f;
        private float _rotationSpeed = 500f;
        private float _riseDistanceThreshold = .5f;

        public EnvIntApproachState(EnvironmentInteractionContext context,
            EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate) : base(context, estate)
        { }

        public override void EnterState()
        {
            _elapsedTime = 0.0f;
        }
        public override void ExitState(){}
        public override void UpdateState()
        {
            Quaternion expectedGroundRotation = Quaternion.LookRotation(-Vector3.up, Context.RootTransform.forward);
            
            Context.CurrentIKTargetTransform.rotation = Quaternion.RotateTowards(Context.CurrentIKTargetTransform.rotation, 
                expectedGroundRotation, 
                _rotationSpeed*Time.deltaTime);
            
            _elapsedTime += Time.deltaTime;
            
            Context.CurrentMultiRotationConstraint.weight = Mathf.Lerp(Context.CurrentMultiRotationConstraint.weight, _approachRotationWeight,
                _elapsedTime / _lerpDuration);
            Context.CurrentIKConstraint.weight = Mathf.Lerp(Context.CurrentIKConstraint.weight, _approachWeight,
                _elapsedTime / _lerpDuration);
        }

        public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState()
        {
            bool isOverStateLifeDuration = _elapsedTime >= _approachDuration;
            
            if (isOverStateLifeDuration || CheckShouldReset())
                return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Reset;
            
            bool isWithinArmsReach = Vector3.Distance(Context.ClosestPointOnColliderFromShoulder,
                Context.CurrentShoulderTransform.position) < _riseDistanceThreshold;
            bool isClosestPointOnColliderReal = Context.ClosestPointOnColliderFromShoulder != Vector3.positiveInfinity;
            
            if(isWithinArmsReach && isClosestPointOnColliderReal)
                return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Rise;
            
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