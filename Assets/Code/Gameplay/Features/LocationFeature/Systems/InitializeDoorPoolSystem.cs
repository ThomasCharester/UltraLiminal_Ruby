using System;
using System.Collections.Generic;
using Code.Gameplay.Common.Pooler;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class InitializeDoorPoolSystem: IInitializeSystem
    {
        private readonly IDoorPoolerService _doorPoolerService;
        private readonly IStaticDataService _staticDataService;

        public InitializeDoorPoolSystem(IDoorPoolerService doorPoolerService,
            IStaticDataService staticDataService)
        {
            _doorPoolerService = doorPoolerService;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            foreach (DoorID segmentID in Enum.GetValues(typeof(DoorID)))
            {
                // Totally unsafe lol
                // int count = _staticDataService.LocationSegmentsCountInPoolConfig.SegmentCount.ContainsKey(segmentID)
                //     ? _staticDataService.LocationSegmentsCountInPoolConfig.SegmentCount[segmentID]
                //     : 2;
                
                // Сделай конфиг
                int count = 5;
                
                List<GameEntity> poolShit = new(count);
                for (int i = 0; i < count; i++)
                    poolShit.Add(_doorPoolerService.GetPool(segmentID).Get());

                for (int i = 0; i < count; i++)
                    _doorPoolerService.GetPool(segmentID).Release(poolShit[i]);
            }
        }
    }
}