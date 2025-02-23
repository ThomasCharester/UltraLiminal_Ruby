using System.Collections.Generic;
using Code.Gameplay.Common.Pooler;
using Entitas;

namespace Code.Gameplay.Features.Common.Systems
{
    public class ActiveOnSceneToUnitySystem: IExecuteSystem
    {
        private List<GameEntity> buffer = new(32);
        private readonly IGroup<GameEntity> _activeOrNot;

        public ActiveOnSceneToUnitySystem(GameContext game, ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _activeOrNot = game.GetGroup(GameMatcher.AllOf(GameMatcher.View));
        }

        public void Execute()
        {
            foreach (var activeOrNot in _activeOrNot.GetEntities(buffer))
            {
                if(activeOrNot.isActiveOnScene != activeOrNot.View.gameObject.activeSelf)
                    activeOrNot.View.gameObject.SetActive(activeOrNot.isActiveOnScene);
            }
        }
    }
}