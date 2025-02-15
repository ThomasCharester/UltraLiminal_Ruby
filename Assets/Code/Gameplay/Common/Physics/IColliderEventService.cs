using System.Collections.Generic;

namespace Code.Gameplay.Common.Physics
{
    public interface IColliderEventService
    {
        List<GameEntity> EnteredEntities { get; }
        List<GameEntity> ExitedEntities { get; }
        List<GameEntity> StayingEntities { get; }
    }
}