using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Common;
using Code.Infrastructure.Indentifiers;
using Entitas.VisualDebugging.Unity;
using UnityEngine;
using EntityBehaviour = Code.Infrastructure.View.EntityBehaviour;

namespace Code.Gameplay.Features.Camera.Factory
{
    public class CameraFactory : ICameraFactory
    {
        private readonly IIndentifierService _indentifierService;

        public CameraFactory(IIndentifierService indentifierService)
        {
            _indentifierService = indentifierService;
        }

        public GameEntity CreateCinemachineCamera(Vector3 position, EntityBehaviour cinemachineCameraPrefab)
        {
            return CreateEntity.Empty()
                .AddId(_indentifierService.NextId())
                .AddSpawnPoint(position)
                .AddViewPrefab(cinemachineCameraPrefab)
                .With(x => x.isCamera = true)
                .With(x => x.isCinemachine = true);
        }
        public GameEntity CreateMainCamera(Vector3 position, EntityBehaviour mainCameraPrefab)
        {
            return CreateEntity.Empty()
                .AddId(_indentifierService.NextId())
                .AddSpawnPoint(position)
                .AddViewPrefab(mainCameraPrefab)
                .With(x => x.isCamera = true);
        }
    }
}