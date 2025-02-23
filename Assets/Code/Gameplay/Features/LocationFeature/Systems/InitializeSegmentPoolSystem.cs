using System;
using System.Collections.Generic;
using Code.Gameplay.Common.Pooler;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class InitializeSegmentPoolSystem : IInitializeSystem
    {
        private readonly ILocationSegmentPoolerService _locationSegmentPoolerService;
        private readonly IStaticDataService _staticDataService;

        public InitializeSegmentPoolSystem(ILocationSegmentPoolerService locationSegmentPoolerService,
            IStaticDataService staticDataService)
        {
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            foreach (LocationSegmentID segmentID in Enum.GetValues(typeof(LocationSegmentID)))
            {
                // Totally unsafe lol
                // int count = _staticDataService.LocationSegmentsCountInPoolConfig.SegmentCount.ContainsKey(segmentID)
                //     ? _staticDataService.LocationSegmentsCountInPoolConfig.SegmentCount[segmentID]
                //     : 2;
                
                int count = _staticDataService.LocationSegmentsCountInPoolConfig.SegmentCount[(int)segmentID];
                
                List<GameEntity> poolShit = new(count);
                for (int i = 0; i < count; i++)
                    poolShit.Add(_locationSegmentPoolerService.GetPool(segmentID).Get());

                for (int i = 0; i < count; i++)
                    _locationSegmentPoolerService.GetPool(segmentID).Release(poolShit[i]);
            }
        }
    }
}