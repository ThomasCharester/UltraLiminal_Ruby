using Code.Gameplay.Common.Collisions;
using Code.Infrastructure.View.Registrar;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View
{
    public class EntityBehaviour : MonoBehaviour, IEntityView
    {
        private GameEntity _entity;
        private ICollisionRegistry _collisionRegistry;

        public GameEntity Entity => _entity;

        [Inject]
        private void Construct(ICollisionRegistry collisionRegistry) => 
            _collisionRegistry = collisionRegistry;
        
        public void SetEntity(GameEntity entity)
        {
            _entity = entity;
            _entity.AddView(this);
            _entity.Retain(this);

            foreach (var registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.RegisterComponents();
            
            foreach (Collider colider in GetComponentsInChildren<Collider>(includeInactive: true)) 
                _collisionRegistry.Register(colider.GetInstanceID(), _entity);
        }

        public void ReleaseEntity()
        {
            foreach (var registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.UnregisterComponents();
            
            foreach (Collider colider in GetComponentsInChildren<Collider>(includeInactive: true)) 
                _collisionRegistry.Unregister(colider.GetInstanceID());
            
            _entity.Release(this);
            _entity = null;
        }
    }
}