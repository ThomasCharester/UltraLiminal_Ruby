using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Camera.Configs;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Camera.Factory
{
    public class CameraFactory : ICameraFactory
    {
        private readonly IIdentifierService _identifierService;

        public CameraFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateCinemachineCamera( in Vector3 position, in CameraConfig cinemachineCameraConfig)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddVectorSpawnPoint(position)
                .AddViewPrefab(cinemachineCameraConfig.cameraPrefab)
                .With(x => x.isCamera = true);
        }
        public GameEntity CreateMainCamera( in Vector3 position, in CameraConfig mainCameraConfig)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddVectorSpawnPoint(position)
                .AddViewPrefab(mainCameraConfig.cameraPrefab)
                .With(x => x.isCamera = true);
        }
    }
}