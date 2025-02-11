using Code.Gameplay.Movement.Controller;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public class DoorInteractionContext
    {
        public enum EBodySide
        {
            Right,
            Left
        }

        private float _lerpDuration;
        private float _rotationSpeed;

        private TwoBoneIKConstraint _leftIKConstraint;
        private TwoBoneIKConstraint _rightIKConstraint;
        private MultiRotationConstraint _leftMultiRotationConstraint;
        private MultiRotationConstraint _rightMultiRotationConstraint;
        private CapsuleCollider _rootCollider;
        private Transform _rootTransform;
        private Transform _leftOriginalTargetTransform;
        private Transform _rightOriginalTargetTransform;
        private Transform _shoulderHeightTransform;

        public DoorInteractionContext(
            TwoBoneIKConstraint leftIKConstraint,
            TwoBoneIKConstraint rightIKConstraint,
            MultiRotationConstraint leftMultiRotationConstraint,
            MultiRotationConstraint rightMultiRotationConstraint,
            StandaloneCharacterController standaloneCharacterController,
            Transform rootTransform,
            Transform leftTransform,
            Transform rightTransform,
            Transform shoulderHeight,
            float lerpDuration,
            float rotationSpeed)
        {
            _leftIKConstraint = leftIKConstraint;
            _rightIKConstraint = rightIKConstraint;
            _leftMultiRotationConstraint = leftMultiRotationConstraint;
            _rightMultiRotationConstraint = rightMultiRotationConstraint;
            _rootCollider = standaloneCharacterController.GetCapsuleCollider();
            _rootTransform = rootTransform;
            _leftOriginalTargetTransform = leftTransform;
            _rightOriginalTargetTransform = rightTransform;

            _shoulderHeightTransform = shoulderHeight;
            _lerpDuration = lerpDuration;
            _rotationSpeed = rotationSpeed;

            SetCurrentSide(Vector3.positiveInfinity);
        }

        public TwoBoneIKConstraint LeftIKConstraint => _leftIKConstraint;
        public TwoBoneIKConstraint RightIKConstraint => _rightIKConstraint;
        public MultiRotationConstraint LeftMultiRotationConstraint => _leftMultiRotationConstraint;
        public MultiRotationConstraint RightMultiRotationConstraint => _rightMultiRotationConstraint;
        public CapsuleCollider RootCollider => _rootCollider;
        public Transform RootTransform => _rootTransform;
        public float LerpDuration => _lerpDuration;
        public float RotationSpeed => _rotationSpeed;
        
        public float CharacterShoulderHeight => _shoulderHeightTransform.position.y;
        public Collider CurrentIntersectingCollider { get; set; }
        public TwoBoneIKConstraint CurrentIKConstraint { get; private set; }
        public MultiRotationConstraint CurrentMultiRotationConstraint { get; private set; }
        public Transform CurrentIKTargetTransform { get; private set; }
        public Transform CurrentShoulderTransform { get; private set; }
        public EBodySide CurrentBodySide { get; private set; }
        public Vector3 ClosestPointOnColliderFromShoulder { get; set; } = Vector3.positiveInfinity;
        public Vector3 CurrentOriginalTargetPosition { get; private set; }

        public void ResetHands(float elapsedTime)
        {
            if (LeftMultiRotationConstraint.weight != 0)
                LeftMultiRotationConstraint.weight = Mathf.Lerp(LeftMultiRotationConstraint.weight, 0,
                    elapsedTime / _lerpDuration);
            if (LeftIKConstraint.weight != 0)
                LeftIKConstraint.weight = Mathf.Lerp(LeftIKConstraint.weight, 0, elapsedTime / _lerpDuration);

            if (_leftOriginalTargetTransform.localPosition != LeftIKConstraint.data.target.localPosition)
            {
                LeftIKConstraint.data.target.transform.localPosition = Vector3.Lerp(
                    LeftIKConstraint.data.target.transform.localPosition,
                    _leftOriginalTargetTransform.localPosition, elapsedTime / _lerpDuration);
            }

            if (LeftIKConstraint.data.target.rotation != _leftOriginalTargetTransform.rotation)
            {
                LeftIKConstraint.data.target.rotation = Quaternion.RotateTowards(
                    LeftIKConstraint.data.target.rotation,
                    _leftOriginalTargetTransform.rotation, _rotationSpeed * Time.deltaTime);
            }

            if (RightMultiRotationConstraint.weight != 0)
                RightMultiRotationConstraint.weight = Mathf.Lerp(RightMultiRotationConstraint.weight, 0,
                    elapsedTime / _lerpDuration);
            if (RightIKConstraint.weight != 0)
                RightIKConstraint.weight = Mathf.Lerp(RightIKConstraint.weight, 0, elapsedTime / _lerpDuration);

            if (_rightOriginalTargetTransform.localPosition == RightIKConstraint.data.target.localPosition)
            {
                RightIKConstraint.data.target.transform.localPosition = Vector3.Lerp(
                    RightIKConstraint.data.target.transform.localPosition,
                    _rightOriginalTargetTransform.localPosition, elapsedTime / _lerpDuration);
            }

            if (RightIKConstraint.data.target.rotation == _rightOriginalTargetTransform.rotation)
            {
                RightIKConstraint.data.target.rotation = Quaternion.RotateTowards(
                    RightIKConstraint.data.target.rotation,
                    _rightOriginalTargetTransform.rotation, _rotationSpeed * Time.deltaTime);
            }
        }

        public void ResetHand(EBodySide side, float elapsedTime)
        {
            switch (side)
            {
                case EBodySide.Left:

                    if (LeftMultiRotationConstraint.weight != 0)
                        LeftMultiRotationConstraint.weight = Mathf.Lerp(LeftMultiRotationConstraint.weight, 0,
                            elapsedTime / _lerpDuration);
                    if (LeftIKConstraint.weight != 0)
                        LeftIKConstraint.weight = Mathf.Lerp(LeftIKConstraint.weight, 0, elapsedTime / _lerpDuration);

                    if (_leftOriginalTargetTransform.localPosition != LeftIKConstraint.data.target.localPosition)
                    {
                        LeftIKConstraint.data.target.transform.localPosition = Vector3.Lerp(
                            LeftIKConstraint.data.target.transform.localPosition,
                            _leftOriginalTargetTransform.localPosition, elapsedTime / _lerpDuration);
                    }

                    if (LeftIKConstraint.data.target.rotation != _leftOriginalTargetTransform.rotation)
                    {
                        LeftIKConstraint.data.target.rotation = Quaternion.RotateTowards(
                            LeftIKConstraint.data.target.rotation,
                            _leftOriginalTargetTransform.rotation, _rotationSpeed * Time.deltaTime);
                    }

                    break;
                case EBodySide.Right:

                    if (RightMultiRotationConstraint.weight != 0)
                        RightMultiRotationConstraint.weight = Mathf.Lerp(RightMultiRotationConstraint.weight, 0,
                            elapsedTime / _lerpDuration);
                    if (RightIKConstraint.weight != 0)
                        RightIKConstraint.weight = Mathf.Lerp(RightIKConstraint.weight, 0, elapsedTime / _lerpDuration);

                    if (_rightOriginalTargetTransform.localPosition == RightIKConstraint.data.target.localPosition)
                    {
                        RightIKConstraint.data.target.transform.localPosition = Vector3.Lerp(
                            RightIKConstraint.data.target.transform.localPosition,
                            _rightOriginalTargetTransform.localPosition, elapsedTime / _lerpDuration);
                    }

                    if (RightIKConstraint.data.target.rotation == _rightOriginalTargetTransform.rotation)
                    {
                        RightIKConstraint.data.target.rotation = Quaternion.RotateTowards(
                            RightIKConstraint.data.target.rotation,
                            _rightOriginalTargetTransform.rotation, _rotationSpeed * Time.deltaTime);
                    }

                    break;
            }
        }

        public void SetCurrentSide(Vector3 positionToCheck)
        {
            Vector3 leftShoulder = _leftIKConstraint.data.root.transform.position;
            Vector3 rightShoulder = _rightIKConstraint.data.root.transform.position;

            bool isLeftCloser = Vector3.Distance(positionToCheck, leftShoulder) <
                                Vector3.Distance(positionToCheck, rightShoulder);
            if (isLeftCloser)
            {
                CurrentBodySide = EBodySide.Left;
                CurrentIKConstraint = _leftIKConstraint;
                CurrentMultiRotationConstraint = _leftMultiRotationConstraint;
                CurrentOriginalTargetPosition = _leftOriginalTargetTransform.localPosition;
            }
            else
            {
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