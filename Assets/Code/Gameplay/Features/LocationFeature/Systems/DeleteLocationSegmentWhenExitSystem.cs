using System.Linq;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteLocationSegmentWhenExitSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _doorFrames;

        public DeleteLocationSegmentWhenExitSystem(GameContext game)
        {
            _game = game;
            _doorFrames = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.OwnerDoor
            ));
        }

        public void Execute()
        {
            foreach (var frame in _doorFrames)
            {
                if (frame.TriggerEventService.ExitedEntities.Count <= 0
                    || frame.TriggerEventService.ExitedEntities.Where(x => x.isPlayer).ToList().First() ==
                    null) continue;

                frame.TriggerEventService.ExitedEntities.Clear();

                GameEntity slaveSegment =
                    _game.GetEntityWithId(_game.GetEntityWithId(frame.OwnerDoor).SlaveLocationSegment);
                GameEntity masterSegment =
                    _game.GetEntityWithId(_game.GetEntityWithId(frame.OwnerDoor).MasterLocationSegment);

                _game.GetEntityWithId(frame.OwnerDoor).RemoveSlaveLocationSegment();

                if (masterSegment.TriggerEventService.StayingEntities.Count <= 0 && masterSegment.TriggerEventService
                        .StayingEntities.Where(x => x.isPlayer).ToList().First() == null)
                {
                    _game.GetEntityWithId(frame.OwnerDoor).ReplaceMasterLocationSegment(slaveSegment.Id);
                    masterSegment.isDestructed = true;
                }
                else slaveSegment.isDestructed = true;
            }
        }
    }
}