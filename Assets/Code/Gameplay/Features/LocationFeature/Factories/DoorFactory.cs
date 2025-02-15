using Code.Common.Entity;
using Code.Common.Extensions;
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
                .AddMasterLocationSegment(masterID)
                .AddVectorSpawnPoint(originPosition)
                .AddRotationSpawnPoint(originRotation)
                .AddViewPrefab(_staticDataService.GetDoorConfig(segmentID).doorPrefab)
                .With(x => x.isDoorOff = true);
        }
    }
}