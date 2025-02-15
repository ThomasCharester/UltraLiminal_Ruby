using Code.Gameplay.StaticData;
using Code.Infrastructure.ShitManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View.Factory
{
    public class EntityViewFactory : IEntityViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public EntityViewFactory(IInstantiator instantiator, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public IEntityView CreateEntityViewFromPrefab(GameEntity entity)
        {
            EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>
                (
                    entity.ViewPrefab,
                    position: _staticDataService.GameplayConstantsConfig._farAway,
                    Quaternion.identity, 
                    parentTransform: null
                    );
            
            view.SetEntity(entity);
            
            return view;
        }
        public IEntityView CreateEntityViewFromPath(GameEntity entity)
        {
            EntityBehaviour prefab = _assetProvider.LoadAsset<EntityBehaviour>(entity.ViewPath);
            EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>
            (
                prefab,
                position: _staticDataService.GameplayConstantsConfig._farAway,
                Quaternion.identity, 
                parentTransform: null
            );
            
            view.SetEntity(entity);
            
            return view;
        }
    }
}