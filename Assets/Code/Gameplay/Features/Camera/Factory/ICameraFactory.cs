using Code.Gameplay.Features.Camera.Configs;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Camera.Factory
{
    public interface ICameraFactory
    {
        GameEntity CreateCinemachineCamera( in Vector3 position, in CameraConfig cinemachineCameraPrefab);
        GameEntity CreateMainCamera( in Vector3 position, in CameraConfig mainCameraPrefab);
    }
}