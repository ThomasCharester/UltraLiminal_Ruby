using Code.Gameplay.Common;
using Code.Gameplay.Features.Camera.Factory;
using Code.Gameplay.Features.Player.Factory;
using Code.Gameplay.Level;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Camera.Systems
{
    public class InitializeCameras : IInitializeSystem
    {
        private readonly ICameraFactory _cameraFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IStaticDataService _staticDataService;

        public InitializeCameras(ICameraFactory cameraFactory, ILevelDataProvider levelDataProvider, IStaticDataService staticDataService)
        {
            _cameraFactory = cameraFactory;
            _levelDataProvider = levelDataProvider;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            _cameraFactory.CreateMainCamera(_levelDataProvider.PlayerStart,_staticDataService.GetCameraConfig(CameraID.PlayerMainCamera));
            _cameraFactory.CreateCinemachineCamera(_levelDataProvider.PlayerStart,_staticDataService.GetCameraConfig(CameraID.PlayerCinematicCamera));
        }
    }
}