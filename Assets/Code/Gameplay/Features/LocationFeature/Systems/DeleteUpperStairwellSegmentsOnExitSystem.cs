using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Pooler;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteUpperStairwellSegmentsOnExitSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _stairwellSegments;
        private List<GameEntity> buff = new(8);
        private ILocationSegmentPoolerService _locationSegmentPoolerService;

        public DeleteUpperStairwellSegmentsOnExitSystem(GameContext game,
            ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _game = game;
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _stairwellSegments = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.Stairwell,
                GameMatcher.LowerStairwellID)
                .NoneOf(GameMatcher.UpperStairwellID));
        }

        public void Cleanup()
        {
            foreach (var segment in _stairwellSegments.GetEntities(buff))
            {
                if (segment.TriggerEventService.StayingEntities.Any(x => x.isPlayer)) continue;

                GameEntity lowerSegment = _game.GetEntityWithId(segment.LowerStairwellID);

                if (!lowerSegment.hasTriggerEventService) continue;
                
                if (!lowerSegment.TriggerEventService.StayingEntities.Any(x => x.isPlayer))
                {
                    lowerSegment.RemoveUpperStairwellID();

                    segment.RemoveLowerStairwellID();
                }


                if (!segment.hasLowerStairwellID && !segment.hasUpperStairwellID)
                    _locationSegmentPoolerService.GetPool(LocationSegmentID.Stairwell).Release(segment);
            }
        }
    }
}