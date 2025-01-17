using System.Collections.Generic;
using Entitas;

namespace Code.Common.Destruct.Systems
{
    public class CleanupDestructedSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _destructed;
        private List<GameEntity> _buffer = new(64);

        public CleanupDestructedSystem(GameContext game)
        {
            _destructed = game.GetGroup(GameMatcher.Destructed);
        }

        public void Execute()
        {
            foreach (var destructed in _destructed.GetEntities(_buffer))
                destructed.Destroy();
        }
    }
}