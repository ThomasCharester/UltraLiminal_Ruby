using System.Collections.Generic;
using Code.Gameplay.Features.LocationFeature.Factories;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class SetupDoorsAfterSpawnSystem : IExecuteSystem
    {
        private readonly IDoorFactory _doorFactory;
        private readonly IGroup<GameEntity> _doorNeededSegments;
        private List<GameEntity> buff = new(8);

        public SetupDoorsAfterSpawnSystem(GameContext game, IDoorFactory doorFactory)
        {
            _doorFactory = doorFactory;
            _doorNeededSegments = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.NeedSomeDoors, GameMatcher.LocationSegment, GameMatcher.Transform)
                .NoneOf(GameMatcher.VectorSpawnPoint, GameMatcher.RotationSpawnPoint));
        }

        public void Execute()
        {
            foreach (var doorNeededSegment in _doorNeededSegments.GetEntities(buff))
            {
                if (doorNeededSegment.hasBadDoorId)
                {
                    _doorFactory.SpawnDoors(doorNeededSegment.Transform.position, doorNeededSegment.Transform.rotation,
                        doorNeededSegment.LocationSegment, doorNeededSegment.Id,
                        doorNeededSegment.BadDoorId);
                    doorNeededSegment.RemoveBadDoorId();
                }
                else
                    _doorFactory.SpawnDoors(doorNeededSegment.Transform.position, doorNeededSegment.Transform.rotation,
                        doorNeededSegment.LocationSegment, doorNeededSegment.Id);

                doorNeededSegment.isNeedSomeDoors = false;
            }
        }
    }
}