using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public class DoorFactory : IDoorFactory
    {
        
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public DoorFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateDoor(DoorID segmentID, Vector3 originPosition, Quaternion originRotation, int masterID)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddVectorSpawnPoint(originPosition)
                .AddMasterLocationSegment(masterID)
                .AddViewPrefab(_staticDataService.GetDoorConfig(segmentID).doorPrefab);
        }
    }
}