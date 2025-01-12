using UnityEngine;
using Code.Gameplay.Movement.Controller;
using Entitas;

namespace Code.Gameplay.Features.Player.Systems
{
    public class SetPlayerInputsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _inputs;
        private readonly IGroup<GameEntity> _camera;

        public SetPlayerInputsSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.CharacterController));
            _inputs = game.GetGroup(GameMatcher.Input);
            _camera = game.GetGroup(GameMatcher.MainCamera);
        }

        public void Execute()
        {
            foreach (var entity in _entities)
                if (_camera.GetEntities().Length > 0)
                    foreach (var camera in _camera)
                        if (_inputs.GetEntities().Length > 0)
                            foreach (var input in _inputs)
                            {
                                if(input.hasAxisInput)
                                    entity.ReplacePlayerInputs(new PlayerCharacterInputs(input.AxisInput,
                                    camera.Transform.rotation, input.isJumpButtonHold, input.isCrouchButtonHold, input.isCrouchButtonHold));
                                else
                                    entity.ReplacePlayerInputs(new PlayerCharacterInputs(Vector2.zero,
                                    camera.Transform.rotation, input.isJumpButtonHold, input.isCrouchButtonHold, input.isCrouchButtonHold));
                            }
                        else
                            entity.ReplacePlayerInputs(new PlayerCharacterInputs(Vector2.zero,
                                camera.Transform.rotation, false, false, false));
                else entity.RemovePlayerInputs();
        }
    }
}