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
            _uselessDoors = game.GetGroup(GameMatcher.AllOf(GameMatcher.MasterLocationSegment));
            _uselessDoorFrames = game.GetGroup(GameMatcher.AllOf(GameMatcher.OwnerDoor));
        }
        public void Cleanup()
        {
            foreach (var uselessDoor in _uselessDoors)
            {
                if (_game.GetEntityWithId(uselessDoor.MasterLocationSegment) == null) uselessDoor.isDestructed = true;
            }

            foreach (var uselessDoorFrame in _uselessDoorFrames)
            {
                var ownerDoor = _game.GetEntityWithId(uselessDoorFrame.OwnerDoor);
                if(ownerDoor == null || ownerDoor.isDestructed) uselessDoorFrame.isDestructed = true;
            }
        }
    }
}