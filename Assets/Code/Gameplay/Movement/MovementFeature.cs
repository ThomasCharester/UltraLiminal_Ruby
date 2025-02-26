using Code.Gameplay.Movement.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory systems)
        {
            Add(systems.Create<MoveToTransformSpawnPointSystem>()); // First always
            Add(systems.Create<MoveToVectorSpawnPointSystem>());
            Add(systems.Create<RotateToSpawnPointSystem>());
        }
    }
}