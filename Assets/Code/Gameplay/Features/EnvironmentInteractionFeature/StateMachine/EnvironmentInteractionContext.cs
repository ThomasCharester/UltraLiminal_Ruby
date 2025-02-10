using Code.Gameplay.Movement.Controller;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public class EnvironmentInteractionContext
    {
        public enum EBodySide
        {
            Right,
            Left
        }
        
        private TwoBoneIKConstraint _leftIKConstraint;
        private TwoBoneIKConstraint _rightIKConstraint;
        private MultiRotationConstraint _leftMultiRotationConstraint;
        private MultiRotationConstraint _rightMultiRotationConstraint;
        private Rigidbody _rigidbody;
        private CapsuleCollider _rootCollider;
        private Transform _rootTransform;
        private Transform _leftOriginalTargetTransform;
        private Transform _rightOriginalTargetTransform;
        private Transform _shoulderOrigin;

        public EnvironmentInteractionContext(
            TwoBoneIKConstraint leftIKConstraint,
            TwoBoneIKConstraint rightIKConstraint, 
            MultiRotationConstraint leftMultiRotationConstraint,
            MultiRotationConstraint rightMultiRotationConstraint,
            StandaloneCharacterController standaloneCharacterController, 
            Transform rootTransform,
            Transform leftOriginalTarget,
            Transform rightOriginalTarget,
            Transform shoulderOrigin)
        {
            _leftIKConstraint = leftIKConstraint;
            _rightIKConstraint = rightIKConstraint;
            _leftMultiRotationConstraint = leftMultiRotationConstraint;
            _rightMultiRotationConstraint = rightMultiRotationConstraint;
            _rigidbody = standaloneCharacterController.GetRigidbody();
            _rootCollider = standaloneCharacterController.GetCapsuleCollider();
            _rootTransform = rootTransform;
            _leftOriginalTargetTransform = leftOriginalTarget;
            _rightOriginalTargetTransform = rightOriginalTarget;
            OriginalTargetRotation = _leftIKConstraint.data.target.rotation;
            
            
            _shoulderOrigin = shoulderOrigin;
            SetCurrentSide(Vector3.positiveInfinity);
        }

        public TwoBoneIKConstraint LeftIKConstraint => _leftIKConstraint;
        public TwoBoneIKConstraint RightIKConstraint => _rightIKConstraint;
        public MultiRotationConstraint LeftMultiRotationConstraint => _leftMultiRotationConstraint;
        public MultiRotationConstraint RightMultiRotationConstraint => _rightMultiRotationConstraint;
        public Rigidbody Rigidbody => _rigidbody;
        public CapsuleCollider RootCollider => _rootCollider;
        public Transform RootTransform => _rootTransform;
        
        public float CharacterShoulderHeight => _shoulderOrigin.localPosition.y;
        public Collider CurrentIntersectingCollider { get; set; }
        public TwoBoneIKConstraint CurrentIKConstraint { get; private set; }
        public MultiRotationConstraint CurrentMultiRotationConstraint { get; private set; }
        public Transform CurrentIKTargetTransform { get; private set; }
        public Transform CurrentShoulderTransform { get; private set; }
        public EBodySide CurrentBodySide { get; private set; }
        public Vector3 ClosestPointOnColliderFromShoulder { get; set; } = Vector3.positiveInfinity;
        public float InteractionPointYOffset { get; set; } = 0;
        public float ColliderCenterY { get; set; }
        public Vector3 CurrentOriginalTargetPosition { get; private set; }
        public Quaternion OriginalTargetRotation { get; private set; }


        public void SetCurrentSide(Vector3 positionToCheck)
        {
            Vector3 leftShoulder = _leftIKConstraint.data.root.transform.position;
            Vector3 rightShoulder = _rightIKConstraint.data.root.transform.position;
            
            bool isLeftCloser = Vector3.Distance(positionToCheck,leftShoulder) < Vector3.Distance(positionToCheck, rightShoulder);
            if (isLeftCloser)
            {
                Debug.Log("LeftCloser");
                CurrentBodySide = EBodySide.Left;
                CurrentIKConstraint = _leftIKConstraint;
                CurrentMultiRotationConstraint = _leftMultiRotationConstraint;
                CurrentOriginalTargetPosition = _leftOriginalTargetTransform.localPosition;
            }
            else
            {
                Debug.Log("RightCloser");
                CurrentBodySide = EBodySide.Right;
                CurrentIKConstraint = _rightIKConstraint;
                CurrentMultiRotationConstraint = _rightMultiRotationConstraint;
                CurrentOriginalTargetPosition = _rightOriginalTargetTransform.localPosition;
            }
            
            CurrentShoulderTransform = CurrentIKConstraint.data.root.transform;
            CurrentIKTargetTransform = CurrentIKConstraint.data.target.transform;
        }
    }
}