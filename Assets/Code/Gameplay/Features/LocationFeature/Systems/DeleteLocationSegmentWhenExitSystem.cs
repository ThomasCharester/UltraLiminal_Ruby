using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteLocationSegmentWhenExitSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _doorFrames;
        private List<GameEntity> buff = new(8);

        public DeleteLocationSegmentWhenExitSystem(GameContext game)
        {
            _game = game;
            _doorFrames = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.MasterLocationSegment,
                GameMatcher.SlaveLocationSegment
            ));
        }

        public void Execute()
        {
            foreach (var frame in _doorFrames.GetEntities(buff))
            {
                if (frame.TriggerEventService.ExitedEntities.Count <= 0
                    || frame.TriggerEventService.ExitedEntities.Where(x => x.isPlayer).ToList().First() ==
                    null) continue;

                frame.TriggerEventService.ExitedEntities.Clear();

                GameEntity slaveSegment =
                    _game.GetEntityWithId(frame.SlaveLocationSegment);
                GameEntity masterSegment =
                    _game.GetEntityWithId(frame.MasterLocationSegment);

                frame.RemoveSlaveLocationSegment();

                if (masterSegment.TriggerEventService.StayingEntities.Count <= 0 
                    || !masterSegment.TriggerEventService.StayingEntities.Any(x => x.isPlayer))
                {
                    frame.ReplaceMasterLocationSegment(slaveSegment.Id);
                    masterSegment.isDestructed = true;
                    
                    frame.Transform.rotation = Quaternion.Euler(0,180f - frame.Transform.rotation.eulerAngles.y,0);
                }
                else slaveSegment.isDestructed = true;
            }
        }
    }
}