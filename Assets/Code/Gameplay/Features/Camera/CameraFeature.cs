using Code.Gameplay.Features.Camera.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Camera
{
    public class CameraFeature: Feature
    {
        public CameraFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeCameras>());
            Add(systems.Create<SetCameraTrackingTarget>());
        }
    }
}