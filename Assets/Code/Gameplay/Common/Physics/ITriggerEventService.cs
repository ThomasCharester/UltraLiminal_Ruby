using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;

namespace Code.Gameplay.Common.Physics
{
    public interface ITriggerEventService
    {
        List<GameEntity> EnteredEntities { get; }
        List<GameEntity> ExitedEntities { get; }
        List<GameEntity> StayingEntities { get; }

        void SetCollisionRegistry(ICollisionRegistry registry); // АХХАХАХАХАХАХАХАХХАХАХАХАХАХАХХАХАХАХАХАХАХАХАХАПХАХАХАХАХХАХАХА
    }
}