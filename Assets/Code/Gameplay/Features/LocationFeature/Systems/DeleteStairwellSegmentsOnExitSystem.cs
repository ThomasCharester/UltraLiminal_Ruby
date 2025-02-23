using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Pooler;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteStairwellSegmentsOnExitSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _stairwellSegments;
        private List<GameEntity> buff = new(8);
        private ILocationSegmentPoolerService _locationSegmentPoolerService;

        public DeleteStairwellSegmentsOnExitSystem(GameContext game,
            ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _game = game;
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _stairwellSegments = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.Stairwell
            ));
        }

        public void Cleanup()
        {
            foreach (var segment in _stairwellSegments.GetEntities(buff))
            {
                if (segment.TriggerEventService.StayingEntities.Any(x => x.isPlayer)) continue;

                if (segment.hasUpperStairwellID)
                {
                    GameEntity upperSegment = _game.GetEntityWithId(segment.UpperStairwellID);

                    if (!upperSegment.TriggerEventService.StayingEntities.Any(x => x.isPlayer))
                    {
                        upperSegment.RemoveLowerStairwellID();

                        segment.RemoveUpperStairwellID();
                    }
                }

                if (segment.hasLowerStairwellID)
                {
                    //_game.GetEntityWithId(segment.LowerStairwellID).RemoveUpperStairwellID();

                    GameEntity lowerSegment = _game.GetEntityWithId(segment.LowerStairwellID);

                    if (!lowerSegment.TriggerEventService.StayingEntities.Any(x => x.isPlayer))
                    {
                        lowerSegment.RemoveUpperStairwellID();

                        segment.RemoveLowerStairwellID();
                    }
                }

                // segment.RemoveLowerStairwellID();
                // segment.RemoveUpperStairwellID();

                if (!segment.hasLowerStairwellID && !segment.hasUpperStairwellID)
                    _locationSegmentPoolerService.GetPool(LocationSegmentID.Stairwell).Release(segment);

                // segment.isDestructed = true;
                //
                // segment.TriggerEventService.ExitedEntities.Clear();
            }
        }
    }
}