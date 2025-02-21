using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Pooler;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class DeleteLocationSegmentOnExitSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly ILocationSegmentPoolerService _locationSegmentPoolerService;
        private readonly IGroup<GameEntity> _doorFrames;
        private List<GameEntity> buff = new(8);

        public DeleteLocationSegmentOnExitSystem(GameContext game, ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _game = game;
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _doorFrames = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TriggerEventService,
                GameMatcher.MasterLocationSegment,
                GameMatcher.SlaveLocationSegment
            ));
        }

        public void Cleanup()
        {
            foreach (var frame in _doorFrames.GetEntities(buff))
            {
                if (!frame.TriggerEventService.ExitedEntities.Any(x => x.isPlayer)) continue;
                // Каждый кадр, чел
                GameEntity slaveSegment =
                    _game.GetEntityWithId(frame.SlaveLocationSegment);
                GameEntity masterSegment =
                    _game.GetEntityWithId(frame.MasterLocationSegment);

                frame.RemoveSlaveLocationSegment();

                if (!masterSegment.isActiveOnScene || !masterSegment.TriggerEventService.StayingEntities.Any(x => x.isPlayer))
                {
                    frame.ReplaceMasterLocationSegment(slaveSegment.Id);
                    
                    frame.Transform.rotation = Quaternion.Euler(0,frame.SlaveSegmentDoorOriginYRotation,0);
                    
                    if(masterSegment.isActiveOnScene)
                        _locationSegmentPoolerService.GetPool(masterSegment.SegmentID).Release(masterSegment);
                }
                else if(slaveSegment.isActiveOnScene) 
                    _locationSegmentPoolerService.GetPool(slaveSegment.SegmentID).Release(slaveSegment);
                
                frame.RemoveSlaveSegmentDoorOriginYRotation();

                frame.TriggerEventService.ExitedEntities.Clear();
            }
        }
    }
}