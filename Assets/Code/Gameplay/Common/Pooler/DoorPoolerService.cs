using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.LocationFeature;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine.Pool;

namespace Code.Gameplay.Common.Pooler
{
    public class DoorPoolerService : IDoorPoolerService
    {
        // Разные двери, предположим
        private Dictionary<DoorID, DoorPool> _doorPools = new();

        public IObjectPool<GameEntity> GetPool(DoorID doorID) =>
            _doorPools[doorID].Pool;
        // Реши надо не надо приват. Totally unsafe shit.

        public DoorPoolerService(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            foreach (DoorID segmentID in Enum.GetValues(typeof(LocationSegmentID)))
            {
                _doorPools.Add(segmentID, new(segmentID, identifierService, staticDataService));
            }
        }
    }

    public class DoorPool
    {
        // Collection checks will throw errors if we try to release an item that is already in the pool.
        private bool _collectionChecks = true;
        private int _maxPoolSize = 4;

        private IObjectPool<GameEntity> _doorPool;

        private IIdentifierService _identifierService;
        private IStaticDataService _staticDataService;
        private DoorID _doorID;

        public DoorPool(DoorID doorID, in IIdentifierService identifierService,
            in IStaticDataService staticDataService)
        {
            _doorID = doorID;
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public IObjectPool<GameEntity> Pool
        {
            get
            {
                return _doorPool ??= new ObjectPool<GameEntity>(CreatePooledDoor, OnTakeFromDoorPool,
                    OnReturnedToDoorPool, OnDestroyDoorPoolObject, _collectionChecks, 3, _maxPoolSize);
            }
        }

        GameEntity CreatePooledDoor()
        {
            var door = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddViewPrefab(_staticDataService.GetDoorConfig(_doorID).doorPrefab)
                .AddDoorID(_doorID)
                .With(x => x.isDoorOff = _doorID != DoorID.DoorFrame)
                .With(x => x.isActiveOnScene = true);

            return door;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToDoorPool(GameEntity entity)
        {
            entity.isActiveOnScene = false;
            
            if (entity.hasRigidbody)
                entity.Rigidbody.isKinematic = true;
            
            if (entity.hasView)
                entity.View.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromDoorPool(GameEntity entity)
        {
            if (entity.hasView)
                entity.View.gameObject.SetActive(true);
            
            if (entity.hasRigidbody)
                entity.Rigidbody.isKinematic = false;
            
            entity.isActiveOnScene = true;

        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyDoorPoolObject(GameEntity entity)
        {
            entity.isDestructed = true;
        }
    }
}