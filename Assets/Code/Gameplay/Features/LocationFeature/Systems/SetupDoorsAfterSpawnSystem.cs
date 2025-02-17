using System.Collections.Generic;
using Code.Gameplay.Features.LocationFeature.Factories;
using Entitas;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class SetupDoorsAfterSpawnSystem : IExecuteSystem
    {
        private readonly ILocationSegmentFactory _locationSegmentFactory;
        private readonly IGroup<GameEntity> _doorNeededSegments;
        private List<GameEntity> buff = new(8);

        public SetupDoorsAfterSpawnSystem(GameContext game, ILocationSegmentFactory locationSegmentFactory)
        {
            _locationSegmentFactory = locationSegmentFactory;
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
                    _locationSegmentFactory.SpawnDoors(doorNeededSegment.Transform, doorNeededSegment.LocationSegment, doorNeededSegment.Id,
                        doorNeededSegment.BadDoorId);
                    doorNeededSegment.RemoveBadDoorId();
                }
                else
                    _locationSegmentFactory.SpawnDoors(doorNeededSegment.Transform, doorNeededSegment.LocationSegment, doorNeededSegment.Id);
                
                doorNeededSegment.isNeedSomeDoors = false;
            }
        }
    }
}