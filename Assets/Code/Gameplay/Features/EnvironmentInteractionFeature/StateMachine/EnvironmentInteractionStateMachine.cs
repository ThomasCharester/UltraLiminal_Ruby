using System;
using Code.Gameplay.Movement.Controller;
using Code.Gameplay.StateMachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Assertions;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class EnvironmentInteractionStateMachine : StateManager<EnvironmentInteractionStateMachine.EEnvironmentInteractionState>
    {
        public enum EEnvironmentInteractionState
        {
            Search,
            Approach,
            Rise,
            Touch,
            Reset
        }

        private EnvironmentInteractionContext _context;
        [SerializeField] private TwoBoneIKConstraint _leftIKConstraint;
        [SerializeField] private TwoBoneIKConstraint _rightIKConstraint;
        [SerializeField] private MultiRotationConstraint _leftMultiRotationConstraint;
        [SerializeField] private MultiRotationConstraint _rightMultiRotationConstraint;
        [SerializeField] private StandaloneCharacterController _standaloneCharacterController;
        // [SerializeField] private Transform _leftOriginalTransform;
        // [SerializeField] private Transform _rightOriginalTransform;
        // [SerializeField] private Transform _shoulderOrigin;
        private BoxCollider _boxCollider;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if(_context != null && _context.ClosestPointOnColliderFromShoulder != Vector3.positiveInfinity) // to do реши
                Gizmos.DrawSphere(_context.ClosestPointOnColliderFromShoulder, .03f);
        }

        public override void InitializeStateMachine()
        {
            ValidateConstraints();
            _context = new EnvironmentInteractionContext(_leftIKConstraint,
                _rightIKConstraint, 
                _rightMultiRotationConstraint, 
                _leftMultiRotationConstraint, 
                _standaloneCharacterController,
                transform);
            InitializeStates();
            ConstructEnvironmentDetectionCollider();
        }

        private void ValidateConstraints()
        {
            Assert.IsNotNull(_leftIKConstraint, "Left IK constraint is not assigned.");
            Assert.IsNotNull(_rightIKConstraint, "Right IK constraint is not assigned.");
            Assert.IsNotNull(_leftMultiRotationConstraint, "Left multi-rotation constraint is not assigned.");
            Assert.IsNotNull(_rightMultiRotationConstraint, "Right multi-rotation constraint is not assigned.");
            Assert.IsNotNull(_standaloneCharacterController, "CharacterController is not assigned.");
        }

        private void InitializeStates()
        {
            States.Add(EEnvironmentInteractionState.Reset, new ResetState(_context, EEnvironmentInteractionState.Reset));
            States.Add(EEnvironmentInteractionState.Touch, new TouchState(_context, EEnvironmentInteractionState.Touch));
            States.Add(EEnvironmentInteractionState.Rise, new RiseState(_context, EEnvironmentInteractionState.Rise));
            States.Add(EEnvironmentInteractionState.Approach, new ApproachState(_context, EEnvironmentInteractionState.Approach));
            States.Add(EEnvironmentInteractionState.Search, new SearchState(_context, EEnvironmentInteractionState.Search));

            CurrentState = States[EEnvironmentInteractionState.Reset];
        }

        private void ConstructEnvironmentDetectionCollider()
        {
            float wingspan = _context.RootCollider.height;
            
            _boxCollider = gameObject.AddComponent<BoxCollider>();
            
            _boxCollider.size = new Vector3(wingspan, wingspan, wingspan);
            _boxCollider.center = new Vector3(_context.RootCollider.center.x, 
                _context.RootCollider.center.y + (.25f * wingspan),
                _context.RootCollider.center.z + (.5f * wingspan));
            _boxCollider.isTrigger = true;

            _context.ColliderCenterY = _context.RootCollider.center.y;
        }
    }
}