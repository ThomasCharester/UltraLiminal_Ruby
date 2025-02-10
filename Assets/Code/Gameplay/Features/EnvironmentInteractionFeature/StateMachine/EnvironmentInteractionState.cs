using Code.Gameplay.StateMachine;
using UnityEngine;

namespace Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine
{
    public abstract class EnvironmentInteractionState : BaseState<EnvironmentInteractionStateMachine.EEnvironmentInteractionState>
    {
        protected EnvironmentInteractionContext Context;
        private bool _shouldReset ;
        private float _movingAwayOffset = 0.005f;

        public EnvironmentInteractionState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState stateKey) : base(stateKey)
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

            bool isMovingAway = CheckIsMovingAway();

            if (isMovingAway)
                return true;
            
            return false;
        }

        private bool CheckIsMovingAway()
        {
            float currentDistanceToTarget =
                Vector3.Distance(Context.RootTransform.position, Context.ClosestPointOnColliderFromShoulder);
            bool isSearchingForNewInteraction = Context.CurrentIntersectingCollider == null;
            if (isSearchingForNewInteraction)
                return false;

            bool isMovingAwayFromTarget = currentDistanceToTarget > _movingAwayOffset;
            if (isMovingAwayFromTarget)
                return true;
            
            return false;
        }

        private Vector3 GetClosestPointCollider(Collider intersectingCollider, Vector3 positionToCheck)
        {
            return intersectingCollider.ClosestPoint(positionToCheck);
        }

        protected void StartIKTargetPositionTracking(Collider intersectingCollider)
        {
            if (intersectingCollider.gameObject.layer != LayerMask.NameToLayer("Environment") || Context.CurrentIntersectingCollider != null) return;
            
            Context.CurrentIntersectingCollider = intersectingCollider;
            Vector3 closestPointFromRoot = GetClosestPointCollider(intersectingCollider, Context.RootTransform.position);
            Context.SetCurrentSide(closestPointFromRoot);
            
            SetIkTargetPosition();
        }
        protected void UpdateIKTargetPosition(Collider intersectingCollider)
        {
            if(intersectingCollider == Context.CurrentIntersectingCollider)
                SetIkTargetPosition();
        }
        protected void ResetIKTargetPositionTracking(Collider intersectingCollider)
        {
            if (intersectingCollider == Context.CurrentIntersectingCollider)
            {
                Context.CurrentIntersectingCollider = null;
                Context.ClosestPointOnColliderFromShoulder = Vector3.positiveInfinity;
            }
        }

        private void SetIkTargetPosition()
        {
            Context.ClosestPointOnColliderFromShoulder = GetClosestPointCollider(Context.CurrentIntersectingCollider, 
                new Vector3(Context.CurrentShoulderTransform.position.x,Context.CharacterShoulderHeight, Context.CurrentShoulderTransform.position.z));
            
            Vector3 rayDirection = Context.CurrentShoulderTransform.position - Context.ClosestPointOnColliderFromShoulder;
            Vector3 normalizedRayDirection = rayDirection.normalized;
            float offsetDistance = .05f;
            Vector3 offset = normalizedRayDirection * offsetDistance;
            
            Vector3 offsetPosition = Context.ClosestPointOnColliderFromShoulder + offset;
            Context.CurrentIKTargetTransform.position = new Vector3(offsetPosition.x,Context.InteractionPointYOffset,offsetPosition.z);
            
            //Debug.Log("Closest point" + Context.ClosestPointOnColliderFromShoulder);
            //Debug.Log("Offset" + offsetPosition);
            Debug.Log(Context.CurrentIKTargetTransform.position);
        }
    }
}