using Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine;
using Code.Gameplay.StateMachine;
using UnityEngine;

namespace Code.Gameplay.Features.DoorInteractionFeature
{
    public abstract class DoorInteractionState : BaseState<DoorInteractionStateMachine.EDoorInteractionState>
    {
        protected DoorInteractionContext Context;
        private bool _shouldReset;

        public DoorInteractionState(DoorInteractionContext context,
            DoorInteractionStateMachine.EDoorInteractionState stateKey)
            : base(stateKey)
        {
            Context = context;
        }

        protected bool CheckShouldReset()
        {
            if (_shouldReset)
            {
                _shouldReset = false;
                return true;
            }

            CheckIsBadAngle();
            
            return false;
        }

        private void CheckIsBadAngle()
        {
            if (Context.CurrentIntersectingCollider == null) return;

            Vector3 targetDirection =
                Context.ClosestPointOnColliderFromShoulder - Context.CurrentShoulderTransform.position;
            Vector3 shoulderDirection = Context.CurrentBodySide == DoorInteractionContext.EBodySide.Right
                ? Context.RootTransform.right
                : -Context.RootTransform.right;

            float dotProduct = Vector3.Dot(shoulderDirection, targetDirection.normalized); 
            bool isBadAngle = dotProduct < 0f;

            if (isBadAngle)
            {
                Vector3 closestPointFromRoot =
                    GetClosestPointCollider(Context.CurrentIntersectingCollider, Context.RootTransform.position);

                Context.SetCurrentSide(closestPointFromRoot);
            }
        }

        private Vector3 GetClosestPointCollider(Collider intersectingCollider, Vector3 positionToCheck)
        {
            return intersectingCollider.ClosestPoint(positionToCheck);
        }

        protected void StartIKTargetPositionTracking(Collider intersectingCollider)
        {
            if (intersectingCollider.gameObject.layer != LayerMask.NameToLayer("Doors")
                || Context.CurrentIntersectingCollider != null) return;

            Context.CurrentIntersectingCollider = intersectingCollider;
            Vector3 closestPointFromRoot =
                GetClosestPointCollider(intersectingCollider, Context.RootTransform.position);
            Context.SetCurrentSide(closestPointFromRoot);

            SetIkTargetPosition();
        }

        protected void UpdateIKTargetPosition(Collider intersectingCollider)
        {
            if (intersectingCollider == Context.CurrentIntersectingCollider)
                SetIkTargetPosition();
        }

        protected void ResetIKTargetPositionTracking(Collider intersectingCollider)
        {
            if (intersectingCollider == Context.CurrentIntersectingCollider)
            {
                Context.CurrentIntersectingCollider = null;
                Context.ClosestPointOnColliderFromShoulder = Vector3.positiveInfinity;
                _shouldReset = true;
            }
        }

        private void SetIkTargetPosition() 
        {
            Context.ClosestPointOnColliderFromShoulder = GetClosestPointCollider(Context.CurrentIntersectingCollider,
                new Vector3(Context.CurrentShoulderTransform.position.x, Context.CharacterShoulderHeight,
                    Context.CurrentShoulderTransform.position.z));

            Vector3 rayDirection =
                Context.CurrentShoulderTransform.position - Context.ClosestPointOnColliderFromShoulder;
            Vector3 normalizedRayDirection = rayDirection.normalized;
            float offsetDistance = .05f;
            Vector3 offset = normalizedRayDirection * offsetDistance;

            Vector3 offsetPosition = Context.ClosestPointOnColliderFromShoulder + offset;
            Context.CurrentIKTargetTransform.position = offsetPosition;
        }
    }
}