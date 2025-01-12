using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Player.Systems
{
    public class SetPlayerAnimationMovementSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _axisInputs;

        public SetPlayerAnimationMovementSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.PlayerAnimator));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                if(entity.hasDirection)
                    entity.PlayerAnimator.SetMovement(entity.Direction.magnitude);
                else
                    entity.PlayerAnimator.SetMovement(0f);
            }
        }
    }
}