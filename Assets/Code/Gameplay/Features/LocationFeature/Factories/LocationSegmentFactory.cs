using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public class LocationSegmentFactory
    {
        /////////////////////////////            
        public float doorBoxDepth = 0.1f;
        /////////////////////////////            

        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;
        private readonly IDoorFactory _doorFactory;

        public LocationSegmentFactory(IIdentifierService identifierService, IStaticDataService staticDataService, IDoorFactory doorFactory)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
            _doorFactory = doorFactory;
        }

        public GameEntity CreateLocationSegment(LocationSegmentID segmentID, Transform origin)
        {
            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddTransformSpawnPoint(origin)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);

            foreach (var doorOrigin in locationSegment.LocationSegment.GetDoorOrigins)
            {
                Vector3 trueDoorOrigin = doorOrigin.position;
                
                switch (doorOrigin.rotation.y) //
                {
                    case 0f:
                        trueDoorOrigin.x += doorBoxDepth;
                        break;
                    case 90.0f:
                        trueDoorOrigin.z -= doorBoxDepth;
                        break;
                    case 180f:
                        trueDoorOrigin.x -= doorBoxDepth;
                        break;
                    case 270f:
                        trueDoorOrigin.z += doorBoxDepth;
                        break;
                }

                _doorFactory.CreateDoor(DoorID.Default, trueDoorOrigin, doorOrigin.rotation, locationSegment.Id);

            }
            
            return locationSegment;
        }
        
    }
}