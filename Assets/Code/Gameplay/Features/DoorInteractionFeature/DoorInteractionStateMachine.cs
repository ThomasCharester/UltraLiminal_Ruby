using Code.Gameplay.Movement.Controller;
using Code.Gameplay.StateMachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Assertions;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public class DoorInteractionStateMachine: StateManager<DoorInteractionStateMachine.EDoorInteractionState>
    {
        public enum EDoorInteractionState
        {
            Search,
            Approach,
            Rise,
            Touch,
            Reset
        }

        private DoorInteractionContext _context;
        [SerializeField] private TwoBoneIKConstraint _leftIKConstraint;
        [SerializeField] private TwoBoneIKConstraint _rightIKConstraint;
        [SerializeField] private MultiRotationConstraint _leftMultiRotationConstraint;
        [SerializeField] private MultiRotationConstraint _rightMultiRotationConstraint;
        [SerializeField] private StandaloneCharacterController _standaloneCharacterController;
        [SerializeField] private Transform _shoulderOrigin;
        [SerializeField] private Transform _leftOriginalTargetTransform; 
        [SerializeField] private Transform _rightOriginalTargetTransform;
        [SerializeField] private float _lerpDuration = 10.0f;
        [SerializeField] private float _rotationSpeed = 500.0f;
        private BoxCollider _boxCollider;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if(_context != null && _context.ClosestPointOnColliderFromShoulder != Vector3.positiveInfinity) 
                Gizmos.DrawSphere(_context.ClosestPointOnColliderFromShoulder, .03f);
        }

        public override void InitializeStateMachine()
        {
            ValidateConstraints();
            _context = new DoorInteractionContext(_leftIKConstraint,
                _rightIKConstraint, 
                _leftMultiRotationConstraint, 
                _rightMultiRotationConstraint, 
                _standaloneCharacterController,
                transform,
                _leftOriginalTargetTransform,
                _rightOriginalTargetTransform,
                _shoulderOrigin,
                _lerpDuration,
                _rotationSpeed);
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
            States.Add(EDoorInteractionState.Rise, new DoorIntRiseState(_context, EDoorInteractionState.Rise));
            States.Add(EDoorInteractionState.Search, new DoorIntSearchState(_context, EDoorInteractionState.Search));

            CurrentState = States[EDoorInteractionState.Search];
        }

        private void ConstructEnvironmentDetectionCollider() // 
        {
            float wingspan = _context.RootCollider.height;
            
            _boxCollider = gameObject.AddComponent<BoxCollider>();
            
            _boxCollider.size = new Vector3(wingspan * 0.75f , wingspan, wingspan);
            _boxCollider.center = new Vector3(_context.RootCollider.center.x, 
                _context.RootCollider.center.y + (.125f * wingspan),
                _context.RootCollider.center.z + (.5f * wingspan));
            _boxCollider.isTrigger = true;

        }
    }
}