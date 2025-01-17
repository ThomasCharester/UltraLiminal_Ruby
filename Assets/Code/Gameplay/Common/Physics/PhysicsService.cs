using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public class PhysicsService : IPhysicsService
    {
    private static readonly RaycastHit[] Hits = new RaycastHit[128];
    private static readonly Collider[] OverlapHits = new Collider[128];
    private static readonly float MaxDistance = 5f;
    private readonly ICollisionRegistry _collisionRegistry;

    public PhysicsService(ICollisionRegistry collisionRegistry)
    {
      _collisionRegistry = collisionRegistry;
    }

    public IEnumerable<GameEntity> RaycastAll(Vector3 worldPosition, Vector3 direction, int layerMask)
    {
      int hitCount = UnityEngine.Physics.RaycastNonAlloc(worldPosition, direction, Hits, MaxDistance, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public GameEntity Raycast(Vector3 worldPosition, Vector3 direction, int layerMask)
    {
      Debug.DrawRay(worldPosition, direction * MaxDistance, Color.red);
      
      int hitCount = UnityEngine.Physics.RaycastNonAlloc(worldPosition, direction, Hits, MaxDistance, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }
    
    public IEnumerable<GameEntity> SphereCast(Vector3 position, float radius, int layerMask) 
    {
      int hitCount = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, OverlapHits, layerMask);

      DebugExtension.DebugWireSphere(position, Color.red, radius, 1f);
      
      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public int SphereCastNonAllocHitler(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer) 
    {
      int hitCount = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, OverlapHits, layerMask);

      DebugExtension.DebugWireSphere(position, Color.green, radius,1f);
      
      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        if (i < hitBuffer.Length)
          hitBuffer[i] = entity;
      }

      return hitCount;
    }

    public int OverlapSphere(Vector3 worldPos, float radius, Collider[] hits, int layerMask) =>
      UnityEngine.Physics.OverlapSphereNonAlloc(worldPos, radius, hits, layerMask);
    
  }
}