using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Systems
{
    public class ProcessPlayerUseSystem : IExecuteSystem
    {
        private readonly IPhysicsService _physicsService;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _inputs;
        private readonly IGroup<GameEntity> _camera;

        public ProcessPlayerUseSystem(GameContext game, IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _inputs = game.GetGroup(GameMatcher.Input);
            _camera = game.GetGroup(GameMatcher.MainCamera);
        }

        public void Execute()
        {
            foreach (var camera in _camera)
            foreach (var input in _inputs)
                if (input.isUseButtonHold) // to do переписать и под NPC
                {
                    GameEntity item = _physicsService.Raycast(
                        camera.Transform.position,
                        camera.Transform.forward,
                        CollisionLayer.Items.AsMask());
                    if (item != null) // Можно заюзать linq но как
                        item.isTriggered = true;
                }
        }
    }
}