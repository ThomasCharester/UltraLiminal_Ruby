using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Player.Systems
{
    public class SetPlayerAnimationMovementSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _inputs;
        private readonly IGroup<GameEntity> _camera;

        public SetPlayerAnimationMovementSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Player,
                GameMatcher.PlayerAnimator,
                GameMatcher.Transform,
                GameMatcher.AnimationSpeed));

            _inputs = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Input,
                GameMatcher.AxisInput));
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
                                entity.PlayerAnimator.SetRotation(camera.Transform.rotation.y -
                                                                  entity.Transform.rotation.y);
                                
                                Debug.Log(input.AxisInput);
                                
                                if (input.AxisInput.y > 0f)
                                    entity.PlayerAnimator.IncreaseForwardVector(_time.DeltaTime * entity.AnimationSpeed);
                                else if (input.AxisInput.y < 0f)
                                    entity.PlayerAnimator.DecreaseForwardVector(_time.DeltaTime * entity.AnimationSpeed);
                                else 
                                    entity.PlayerAnimator.BringForwardVectorToZero(_time.DeltaTime * entity.AnimationSpeed);
                                
                                if (input.AxisInput.x > 0f)
                                    entity.PlayerAnimator.IncreaseRightVector(_time.DeltaTime * entity.AnimationSpeed);
                                else if (input.AxisInput.x < 0f)
                                    entity.PlayerAnimator.DecreaseRightVector(_time.DeltaTime * entity.AnimationSpeed);
                                else
                                    entity.PlayerAnimator.BringRightVectorToZero(_time.DeltaTime * entity.AnimationSpeed);
                            }
                        else
                        {
                            entity.PlayerAnimator.BringForwardVectorToZero(_time.DeltaTime * entity.AnimationSpeed);
                            entity.PlayerAnimator.BringRightVectorToZero(_time.DeltaTime * entity.AnimationSpeed);
                            entity.PlayerAnimator.SetRotation(camera.Transform.rotation.y -
                                                              entity.Transform.rotation.y);
                        }
                else
                {
                    entity.PlayerAnimator.SetForwardMovement(0f);
                    entity.PlayerAnimator.SetSideMovement(0f);
                    entity.PlayerAnimator.SetRotation(0f);
                }
        }
    }
}