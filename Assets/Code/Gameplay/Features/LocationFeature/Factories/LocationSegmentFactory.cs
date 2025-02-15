using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public class LocationSegmentFactory : ILocationSegmentFactory
    {

        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;
        private readonly IDoorFactory _doorFactory;

        public LocationSegmentFactory(IIdentifierService identifierService, IStaticDataService staticDataService, IDoorFactory doorFactory)
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
                
                switch (doorOrigin.rotation.y) //
                {
                    case 0f:
                        trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;
                        break;
                    case 90f:
                        trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;;
                        break;
                    case 180f:
                        trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;;
                        break;
                    case 270f:
                        trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;;
                        break;
                }

                _doorFactory.CreateDoor(DoorID.Default, trueDoorOrigin, doorOrigin.rotation, locationSegment.Id);

            }
            
            return locationSegment;
        }
        
    }
}