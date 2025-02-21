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

        public LocationSegmentFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateLocationSegment(LocationSegmentID segmentID, in Vector3 position,in Quaternion rotation)
        {
            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddVectorSpawnPoint(position)
                .AddRotationSpawnPoint(rotation)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);

            return locationSegment;
        }

        public GameEntity CreateRandomLocationSegment(in Vector3 position, in Quaternion rotation)
        {
            LocationSegmentID segmentID = (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max());

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