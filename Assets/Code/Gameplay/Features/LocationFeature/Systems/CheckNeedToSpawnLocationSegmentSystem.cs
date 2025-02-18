using System.Linq;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class CheckNeedToSpawnLocationSegmentSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _doorFrames;

        public CheckNeedToSpawnLocationSegmentSystem(GameContext game)
        {
            _doorFrames = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.MasterLocationSegment
            ));
            
        }
        public void Execute()
        {
            foreach (var frame in _doorFrames)
            {
                if (frame.TriggerEventService.EnteredEntities.Count <= 0 
                    || !frame.TriggerEventService.EnteredEntities.Any(x => x.isPlayer)) continue;
                
                frame.isGotOnTheBall = true;
                frame.TriggerEventService.EnteredEntities.Clear();
            }
        }
    }
}