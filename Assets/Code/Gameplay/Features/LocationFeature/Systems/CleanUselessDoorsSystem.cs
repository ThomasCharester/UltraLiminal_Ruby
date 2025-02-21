using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class CleanUselessDoorsSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _uselessDoorFrames;
        private readonly IGroup<GameEntity> _uselessDoors;

        public CleanUselessDoorsSystem(GameContext game)
        {
            _game = game;
            _uselessDoorFrames = game.GetGroup(GameMatcher.AllOf(GameMatcher.MasterLocationSegment));
            _uselessDoors = game.GetGroup(GameMatcher.AllOf(GameMatcher.OwnerFrame));
        }

        public void Cleanup()
        {
            foreach (var uselessDoorFrame in _uselessDoorFrames)
            {
                var segment = _game.GetEntityWithId(uselessDoorFrame.MasterLocationSegment);
                
                if ( segment == null || !segment.isActiveOnScene)
                    uselessDoorFrame.isDestructed = true;
            }

            foreach (var uselessDoor in _uselessDoors)
            {
                var ownerFrame = _game.GetEntityWithId(uselessDoor.OwnerFrame);
                
                if (ownerFrame == null || ownerFrame.isDestructed) 
                    uselessDoor.isDestructed = true;
            }
        }
    }
}