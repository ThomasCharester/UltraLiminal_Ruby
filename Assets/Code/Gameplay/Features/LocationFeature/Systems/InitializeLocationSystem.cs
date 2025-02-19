using System;
using System.Linq;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.Level;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class InitializeLocationSystem : IInitializeSystem
    {
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ILocationSegmentFactory _locationSegmentFactory;

        public InitializeLocationSystem(ILevelDataProvider levelDataProvider,
            IStaticDataService staticDataService, ILocationSegmentFactory locationSegmentFactory)
        {
            _levelDataProvider = levelDataProvider;
            _staticDataService = staticDataService;
            _locationSegmentFactory = locationSegmentFactory;
        }

        public void Initialize()
        {
            GameEntity firstSegment = _locationSegmentFactory.CreateRandomLocationSegment(
                Vector3.zero, 
                Quaternion.identity);// КОНФИГИИИИИИ
            firstSegment.isNeedSomeDoors = true;
            
            _levelDataProvider.SetPlayerStart(firstSegment.LocationSegment.GetPlayerStart.position);
        }
    }
}