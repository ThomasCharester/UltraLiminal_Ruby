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
        private readonly IIdentifierService _identifierService;

        public CameraFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateCinemachineCamera(Vector3 position, EntityBehaviour cinemachineCameraPrefab)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddVectorSpawnPoint(position)
                .AddViewPrefab(cinemachineCameraPrefab)
                .With(x => x.isCamera = true)
                .With(x => x.isCinemachine = true);
        }
        public GameEntity CreateMainCamera(Vector3 position, EntityBehaviour mainCameraPrefab)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddVectorSpawnPoint(position)
                .AddViewPrefab(mainCameraPrefab)
                .With(x => x.isCamera = true);
        }
    }
}