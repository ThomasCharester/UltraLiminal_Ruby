using System;
using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using Unity.VisualScripting;
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

            return locationSegment;
        }

        public void SpawnDoors(DoorCalculator locationSegment, int id, int exceptionOriginIdInList = -1)
        {
            foreach (var doorOrigin in locationSegment.GetDoorOrigins)
            {
                if (exceptionOriginIdInList > -1 && doorOrigin == locationSegment.GetDoorOrigins[exceptionOriginIdInList]) continue; 
                
                Vector3 trueDoorOrigin = doorOrigin.position;

                if (doorOrigin.rotation.y is >= 0f and < 90f)
                    trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y is >= 90f and < 180f)
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y is >= 180f and < 270f)
                    trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;
                else if (doorOrigin.rotation.y is >= 270f and < 360f)
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;
                _doorFactory.CreateDoor((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length),
                    trueDoorOrigin,
                    doorOrigin.rotation, id);
            }
        }

        public GameEntity CreateRandomLocationSegment(Vector3 position, Quaternion rotation)
        {
            LocationSegmentID segmentID =
                (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max());

            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddVectorSpawnPoint(position)
                .AddRotationSpawnPoint(rotation)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);


            return locationSegment;
        }
    }
}