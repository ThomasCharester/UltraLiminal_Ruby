using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Camera.Factory
{
    public interface ICameraFactory
    {
        GameEntity CreateCinemachineCamera(Vector3 position, EntityBehaviour cinemachineCameraPrefab);
        GameEntity CreateMainCamera(Vector3 position, EntityBehaviour mainCameraPrefab);
    }
}