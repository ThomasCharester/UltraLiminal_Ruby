using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteStairwellSegmentsOnExitSystem: IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _stairwellSegments;
        private List<GameEntity> buff = new(8);

        public DeleteStairwellSegmentsOnExitSystem(GameContext game)
        {
            _game = game;
            _stairwellSegments = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.Stairwell
            ));
        }

        public void Execute()
        {
            foreach (var segment in _stairwellSegments.GetEntities(buff))
            {
                if (segment.TriggerEventService.ExitedEntities.Count <= 0
                    || !segment.TriggerEventService.ExitedEntities.Any(x => x.isPlayer)) continue;

                segment.isDestructed = true;

                if (segment.hasUpperStairwellID)
                    _game.GetEntityWithId(segment.UpperStairwellID).RemoveLowerStairwellID();
                
                if (segment.hasLowerStairwellID)
                    _game.GetEntityWithId(segment.LowerStairwellID).RemoveUpperStairwellID();
            }
        }
    }
}