using System.Collections.Generic;
using Code.Infrastructure.View.Factory;
using Entitas;

namespace Code.Infrastructure.View.Systems
{
    public class BindViewFromPathSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory _viewFactory;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public BindViewFromPathSystem(GameContext game, IEntityViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            _entities = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.ViewPath)
                .NoneOf(GameMatcher.View)
            );
        }
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                _viewFactory.CreateEntityViewFromPath(entity);
            }
        }
    }
}