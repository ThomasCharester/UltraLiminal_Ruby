using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public interface IPhysicsService
    {
        IEnumerable<GameEntity> RaycastAll(Vector3 worldPosition, Vector3 direction, int layerMask);
        GameEntity Raycast(Vector3 worldPosition, Vector3 direction, int layerMask);
        IEnumerable<GameEntity> SphereCast(Vector3 position, float radius, int layerMask);
        int SphereCastNonAllocHitler(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer);
        int OverlapSphere(Vector3 worldPos, float radius, Collider[] hits, int layerMask);
    }
}