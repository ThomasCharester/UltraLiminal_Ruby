using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;

namespace Code.Gameplay.Common.Physics
{
    public interface IMultipleTriggerEventService
    {
        List<GameEntity> GetEnteredEntities(int id);
        List<GameEntity> GetExitedEntities(int id);

        // АХХАХАХАХАХАХАХАХХАХАХАХАХАХАХХАХАХАХАХАХАХАХАХАПХАХАХАХАХХАХАХА
        void SetCollisionRegistry(in ICollisionRegistry registry);
    }
}