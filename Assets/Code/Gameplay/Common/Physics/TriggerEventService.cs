using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Common.Physics
{
    public class TriggerEventService : MonoBehaviour, ITriggerEventService
    {
        private List<GameEntity> _enteredEntities = new();
        private List<GameEntity> _exitedEntities = new();
        private List<GameEntity> _stayingEntities = new();
        public List<GameEntity> EnteredEntities => _enteredEntities;
        public List<GameEntity> ExitedEntities => _exitedEntities;
        public List<GameEntity> StayingEntities => _stayingEntities;

        private ICollisionRegistry _collisionRegistry;

        public void OnTriggerEnter(Collider other)
        {
            GameEntity buff = _collisionRegistry?.Get<GameEntity>(other.GetInstanceID());
            if (buff != null)
                _enteredEntities.Add(buff);
        }

        public void OnTriggerStay(Collider other)
        {
            GameEntity buff = _collisionRegistry?.Get<GameEntity>(other.GetInstanceID());
            if (buff != null && !_stayingEntities.Contains(buff))
                _stayingEntities.Add(buff);
        }

        public void OnTriggerExit(Collider other)
        {
            GameEntity buff = _collisionRegistry?.Get<GameEntity>(other.GetInstanceID());
            if (buff != null)
            {
                _exitedEntities.Add(buff);
                _stayingEntities.Remove(buff);
            }
        }

        // АХХАХАХАХАХАХАХАХХАХАХАХАХАХАХХАХАХАХАХАХАХАХАХАПХАХАХАХАХХАХАХА
        public void SetCollisionRegistry(in ICollisionRegistry registry) 
        {
            _collisionRegistry = registry;
        }
    }
}