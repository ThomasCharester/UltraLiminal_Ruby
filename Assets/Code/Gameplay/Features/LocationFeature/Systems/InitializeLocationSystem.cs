using System;
using System.Linq;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.Level;
using Code.Gameplay.StaticData;
using Entitas;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class InitializeLocationSystem : IInitializeSystem
    {
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ILocationSegmentFactory _locationSegmentFactory;

        public InitializeLocationSystem(GameContext game, ILevelDataProvider levelDataProvider,
            IStaticDataService staticDataService, ILocationSegmentFactory locationSegmentFactory)
        {
            _levelDataProvider = levelDataProvider;
            _staticDataService = staticDataService;
            _locationSegmentFactory = locationSegmentFactory;
        }

        public void Initialize()
        {
            GameEntity firstSegment = _locationSegmentFactory.CreateLocationSegment(
                (LocationSegmentID)UnityEngine.Random.Range(0, 1),//Enum.GetValues(typeof(LocationSegmentID)).Length),
                Vector3.zero, 
                Quaternion.identity);// КОНФИГИИИИИИ
            
            _levelDataProvider.SetPlayerStart(firstSegment.LocationSegment.GetPlayerStart.position);
        }
    }
}