using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public class MultipleTriggerEventService : MonoBehaviour, IMultipleTriggerEventService
    {
        [SerializeField] private List<SubTriggerEventHandler> _subTriggerEventHandlers;
        
        private ICollisionRegistry _collisionRegistry;
        
        public List<GameEntity> GetEnteredEntities(int id)
        {
            return _subTriggerEventHandlers[id].EnteredEntities;
        } 
        public List<GameEntity> GetExitedEntities(int id)
        {
            return _subTriggerEventHandlers[id].ExitedEntities;
        } 
        
        public void SetCollisionRegistry(in ICollisionRegistry registry) // АХХАХАХАХАХАХАХАХХАХАХАХАХАХАХХАХАХАХАХАХАХАХАХАПХАХАХАХАХХАХАХА
        {
            _collisionRegistry = registry;
            foreach (var eventHandler in _subTriggerEventHandlers)
            {
                eventHandler.CollisionRegistry = registry;
            }
        }
    }
}