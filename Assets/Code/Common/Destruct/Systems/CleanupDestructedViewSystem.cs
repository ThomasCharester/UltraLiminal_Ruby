using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Common.Destruct.Systems
{
    public class CleanupDestructedViewSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _destructed;

        public CleanupDestructedViewSystem(GameContext game)
        {
            _destructed = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Destructed,
                GameMatcher.View));
        }

        public void Execute()
        {
            foreach (var destructed in _destructed)
            {
                destructed.View.ReleaseEntity();
                Object.Destroy(destructed.View.gameObject);
            }
        }
    }
}