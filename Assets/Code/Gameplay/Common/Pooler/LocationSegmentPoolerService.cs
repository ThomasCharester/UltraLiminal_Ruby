using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.LocationFeature;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using NUnit.Framework.Constraints;
using UnityEngine.Pool;

namespace Code.Gameplay.Common.Pooler
{
    public class LocationSegmentPoolerService : ILocationSegmentPoolerService
    {
        private Dictionary<LocationSegmentID, LocationSegmentPool> _segmentPools = new();

        public IObjectPool<GameEntity> GetPool(LocationSegmentID locationSegmentID) =>
            _segmentPools[locationSegmentID].Pool;
        // Реши надо не надо приват. Totally unsafe shit.

        public LocationSegmentPoolerService(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            foreach (LocationSegmentID segmentID in Enum.GetValues(typeof(LocationSegmentID)))
            {
                _segmentPools.Add(segmentID, new(segmentID, identifierService, staticDataService));
            }
        }
    }

    public class LocationSegmentPool
    {
        // Collection checks will throw errors if we try to release an item that is already in the pool.
        private bool _collectionChecks = true;
        private int _maxPoolSize = 3;

        private IObjectPool<GameEntity> _segmentPool;

        private IIdentifierService _identifierService;
        private IStaticDataService _staticDataService;
        private LocationSegmentID _segmentID;

        public LocationSegmentPool(LocationSegmentID segmentID, in IIdentifierService identifierService,
            in IStaticDataService staticDataService)
        {
            _segmentID = segmentID;
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public IObjectPool<GameEntity> Pool
        {
            get
            {
                return _segmentPool ??= new ObjectPool<GameEntity>(CreatePooledItem, OnTakeFromPool,
                    OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, 3, _maxPoolSize);
            }
        }

        GameEntity CreatePooledItem()
        {
            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(_segmentID).doorCalculator)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(_segmentID).segmentPrefab)
                .AddSegmentID(_segmentID)
                .With(x => x.isActiveOnScene = true);

            return locationSegment;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(GameEntity entity)
        {
            entity.isActiveOnScene = false;
            if (entity.hasView)
                entity.View.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(GameEntity entity)
        {
            if (entity.hasView)
                entity.View.gameObject.SetActive(true);
            entity.isActiveOnScene = true;
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(GameEntity entity)
        {
            entity.isDestructed = true;
        }
    }
}