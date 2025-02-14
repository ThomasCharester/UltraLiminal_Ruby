using Code.Gameplay.Features.Camera.Configs;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Camera.Factory
{
    public interface ICameraFactory
    {
        GameEntity CreateCinemachineCamera(Vector3 position, CameraConfig cinemachineCameraPrefab);
        GameEntity CreateMainCamera(Vector3 position, CameraConfig mainCameraPrefab);
    }
}