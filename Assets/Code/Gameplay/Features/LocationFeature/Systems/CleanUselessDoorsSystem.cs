using System.Collections.Generic;
using Code.Gameplay.Common.Pooler;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class CleanUselessDoorsSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IDoorPoolerService _doorPoolerService;
        private readonly IGroup<GameEntity> _uselessDoorFrames;
        private readonly IGroup<GameEntity> _uselessDoors;
        private List<GameEntity> _frameBuff = new(8);
        private List<GameEntity> _doorBuff = new(8);

        public CleanUselessDoorsSystem(GameContext game, IDoorPoolerService doorPoolerService)
        {
            _game = game;
            _doorPoolerService = doorPoolerService;
            _uselessDoorFrames = game.GetGroup(GameMatcher.AllOf(GameMatcher.MasterLocationSegment));
            _uselessDoors = game.GetGroup(GameMatcher.AllOf(GameMatcher.OwnerFrame));
        }

        public void Cleanup()
        {
            foreach (var uselessDoorFrame in _uselessDoorFrames.GetEntities(_frameBuff))
            {
                var segment = _game.GetEntityWithId(uselessDoorFrame.MasterLocationSegment);

                if (segment == null || !segment.isActiveOnScene)
                {
                    uselessDoorFrame.RemoveMasterLocationSegment();
                    
                    _doorPoolerService.GetPool(DoorID.DoorFrame).Release(uselessDoorFrame);
                    //uselessDoorFrame.isDestructed = true;
                }
            }

            foreach (var uselessDoor in _uselessDoors.GetEntities(_doorBuff))
            {
                var ownerFrame = _game.GetEntityWithId(uselessDoor.OwnerFrame);

                if (ownerFrame == null || !ownerFrame.isActiveOnScene)
                {
                    uselessDoor.RemoveOwnerFrame();
                    
                    if (uselessDoor.hasHingeJointAnchorPosition)
                        uselessDoor.RemoveHingeJointAnchorPosition();
                    
                    if(uselessDoor.hasHingeJointAnchorRotation)
                        uselessDoor.RemoveHingeJointAnchorRotation();
                    
                    _doorPoolerService.GetPool(uselessDoor.DoorID).Release(uselessDoor);
                }
                //uselessDoor.isDestructed = true;
            }
        }
    }
}