using System;
using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public class LocationSegmentFactory : ILocationSegmentFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;
        private readonly IDoorFactory _doorFactory;

        public LocationSegmentFactory(IIdentifierService identifierService, IStaticDataService staticDataService,
            IDoorFactory doorFactory)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
            _doorFactory = doorFactory;
        }

        public GameEntity CreateLocationSegment(LocationSegmentID segmentID, Vector3 position, Quaternion rotation)
        {
            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddVectorSpawnPoint(position)
                .AddRotationSpawnPoint(rotation)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);

            foreach (var doorOrigin in locationSegment.LocationSegment.GetDoorOrigins)
            {
                Vector3 trueDoorOrigin = doorOrigin.position;

                if (doorOrigin.rotation.y >= 0f && doorOrigin.rotation.y <= 90f)
                    trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y > 90f && doorOrigin.rotation.y <= 180f)
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y > 180f && doorOrigin.rotation.y <= 270f)
                    trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y > 270f && doorOrigin.rotation.y < 360f)
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;
                _doorFactory.CreateDoor((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length), trueDoorOrigin,
                    doorOrigin.rotation, locationSegment.Id);
            }

            return locationSegment;
        }
        public GameEntity CreateRandomLocationSegment(Vector3 position, Quaternion rotation)
        {
            LocationSegmentID segmentID =
                (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Length);
            
            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddVectorSpawnPoint(position)
                .AddRotationSpawnPoint(rotation)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);

            foreach (var doorOrigin in locationSegment.LocationSegment.GetDoorOrigins)
            {
                Vector3 trueDoorOrigin = doorOrigin.position;

                if (doorOrigin.rotation.y >= 0f && doorOrigin.rotation.y <= 90f)
                    trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y > 90f && doorOrigin.rotation.y <= 180f)
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y > 180f && doorOrigin.rotation.y <= 270f)
                    trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y > 270f && doorOrigin.rotation.y < 360f)
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;
                _doorFactory.CreateDoor((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length), trueDoorOrigin,
                    doorOrigin.rotation, locationSegment.Id);
            }

            return locationSegment;
        }
    }
}