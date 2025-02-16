using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Camera.Systems
{
    public class SetCameraTrackingTarget : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _cameras;
        private List<GameEntity> _camerabuffer = new(8);

        public SetCameraTrackingTarget(Contexts contexts) : base(contexts.game)
        {
            _cameras = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Camera, GameMatcher.Cinemachine)
                .NoneOf(GameMatcher.Tracking)
            );
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CameraTrackingTarget);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCameraTrackingTarget;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var target in entities)
            {
                foreach (var camera in _cameras.GetEntities(_camerabuffer)) //
                {
                    camera.CinemachineCamera.Target.TrackingTarget = target.CameraTrackingTarget;
                    camera.isTracking = true;
                }
            }
        }
    }
    // }public class SetCameraTrackingTarget : IExecuteSystem
    // {
    //     private readonly IGroup<GameEntity> _targets;
    //     private readonly IGroup<GameEntity> _cameras;
    //
    //     public SetCameraTrackingTarget(GameContext game)
    //     {
    //         _targets = game.GetGroup(GameMatcher.CameraTrackingTarget);
    //         _cameras = game.GetGroup(GameMatcher
    //             .AllOf(GameMatcher.Camera, GameMatcher.Cinemachine)
    //             .NoneOf(GameMatcher.Tracking)
    //         );
    //     }
    //
    //     public void Execute()
    //     {
    //         foreach (var cinemachineCamera in _cameras)
    //         foreach (var target in _targets)
    //         {
    //             cinemachineCamera.CinemachineCamera.Target.TrackingTarget = target.CameraTrackingTarget;
    //             cinemachineCamera.isTracking = true;
    //         }
    //     }
    // }
}