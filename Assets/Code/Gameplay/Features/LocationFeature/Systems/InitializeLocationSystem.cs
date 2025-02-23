using System.Linq;
using Code.Gameplay.Common.Pooler;
using Code.Gameplay.Level;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class InitializeLocationSystem : IInitializeSystem
    {
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly ILocationSegmentPoolerService _locationSegmentPoolerService;

        public InitializeLocationSystem(ILevelDataProvider levelDataProvider, ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _levelDataProvider = levelDataProvider;
            _locationSegmentPoolerService = locationSegmentPoolerService;
        }

        public void Initialize()
        {
            // GameEntity firstSegment = _locationSegmentFactory.CreateRandomLocationSegment(
            //     Vector3.zero, 
            //     Quaternion.identity);// КОНФИГИИИИИИ

            GameEntity firstSegment = _locationSegmentPoolerService.GetPool(
                    (LocationSegmentID)Random.Range(0,System.Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max()+ 1)).Get();
            firstSegment.AddVectorSpawnPoint(Vector3.zero);
            firstSegment.AddRotationSpawnPoint(Quaternion.identity);
            
            firstSegment.isNeedSomeDoors = true;
            
            _levelDataProvider.SetPlayerStart(firstSegment.LocationSegment.GetPlayerStart.position);
        }
    }
}