using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public class ColliderEventService : MonoBehaviour, IColliderEventService
    {
        private List<GameEntity> _enteredEntities;
        private List<GameEntity> _exitedEntities;
        private List<GameEntity> _stayingEntities;
        public List<GameEntity> EnteredEntities => _enteredEntities;
        public List<GameEntity> ExitedEntities => _exitedEntities;
        public List<GameEntity> StayingEntities => _stayingEntities;
        private readonly ICollisionRegistry _collisionRegistry;

        public ColliderEventService(ICollisionRegistry collisionRegistry)
        {
            _collisionRegistry = collisionRegistry;
        }

        public void OnTriggerEnter(Collider other)
        {
            GameEntity buff = _collisionRegistry.Get<GameEntity>(other.GetInstanceID());
            if (buff != null)
                _enteredEntities.Add(buff);
        }

        public void OnTriggerStay(Collider other)
        {
            GameEntity buff = _collisionRegistry.Get<GameEntity>(other.GetInstanceID());
            if (buff != null && !_stayingEntities.Contains(buff))
                _stayingEntities.Add(buff);
        }

        public void OnTriggerExit(Collider other)
        {
            GameEntity buff = _collisionRegistry.Get<GameEntity>(other.GetInstanceID());
            if (buff != null)
                _exitedEntities.Add(buff);
        }
    }
}