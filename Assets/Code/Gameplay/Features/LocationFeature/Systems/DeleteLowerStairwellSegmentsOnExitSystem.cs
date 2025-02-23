using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Pooler;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteLowerStairwellSegmentsOnExitSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _stairwellSegments;
        private List<GameEntity> buff = new(8);
        private ILocationSegmentPoolerService _locationSegmentPoolerService;

        public DeleteLowerStairwellSegmentsOnExitSystem(GameContext game,
            ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _game = game;
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _stairwellSegments = game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.TriggerEventService,
                    GameMatcher.Stairwell,
                    GameMatcher.UpperStairwellID)
                .NoneOf(GameMatcher.LowerStairwellID));
        }

        public void Cleanup()
        {
            foreach (var segment in _stairwellSegments.GetEntities(buff))
            {
                if (segment.TriggerEventService.StayingEntities.Any(x => x.isPlayer)) continue;

                GameEntity upperSegment = _game.GetEntityWithId(segment.UpperStairwellID);

                if (!upperSegment.hasTriggerEventService) continue;

                if (!upperSegment.TriggerEventService.StayingEntities.Any(x => x.isPlayer))
                {
                    upperSegment.RemoveLowerStairwellID();

                    segment.RemoveUpperStairwellID();
                }

                if (!segment.hasLowerStairwellID && !segment.hasUpperStairwellID)
                    _locationSegmentPoolerService.GetPool(LocationSegmentID.Stairwell).Release(segment);
            }
        }
    }
}