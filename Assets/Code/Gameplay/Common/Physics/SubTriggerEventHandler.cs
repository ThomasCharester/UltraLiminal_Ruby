using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public class SubTriggerEventHandler : MonoBehaviour
    {
        public List<GameEntity> EnteredEntities = new();
        public List<GameEntity> ExitedEntities = new();

        public ICollisionRegistry CollisionRegistry;

        public void OnTriggerEnter(Collider other)
        {
            GameEntity buff = CollisionRegistry?.Get<GameEntity>(other.GetInstanceID());
            if (buff != null)
                EnteredEntities.Add(buff);
        }

        public void OnTriggerExit(Collider other)
        {
            GameEntity buff = CollisionRegistry?.Get<GameEntity>(other.GetInstanceID());
            if (buff != null)
            {
                ExitedEntities.Add(buff);
                if (EnteredEntities.Contains(buff))
                    EnteredEntities.Remove(buff);
            }
        }

        public void
            SetCollisionRegistry(
                ICollisionRegistry registry) // АХХАХАХАХАХАХАХАХХАХАХАХАХАХАХХАХАХАХАХАХАХАХАХАПХАХАХАХАХХАХАХА
        {
            CollisionRegistry = registry;
        }
    }
}