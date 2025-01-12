using Entitas;

namespace Code.Gameplay.Features.Player.Systems
{
    public class SendPlayerInputsToControllerSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public SendPlayerInputsToControllerSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.CharacterController,
                GameMatcher.PlayerInputs));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
                entity.CharacterController.SetInputs(entity.PlayerInputs);
        }
    }
}