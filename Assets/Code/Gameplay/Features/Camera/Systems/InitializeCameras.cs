using Code.Gameplay.Common;
using Code.Gameplay.Features.Camera.Factory;
using Code.Gameplay.Features.Player.Factory;
using Code.Gameplay.Level;
using Entitas;

namespace Code.Gameplay.Features.Camera.Systems
{
    public class InitializeCameras : IInitializeSystem
    {
        private readonly ICameraFactory _cameraFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly GameplayConstants _gameplayConstants;

        public InitializeCameras(ICameraFactory cameraFactory, GameplayConstants gameplayConstants, ILevelDataProvider levelDataProvider)
        {
            _gameplayConstants = gameplayConstants;
            _cameraFactory = cameraFactory;
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _cameraFactory.CreateMainCamera(_levelDataProvider.PlayerStart,_gameplayConstants.mainCameraPrefab);
            _cameraFactory.CreateCinemachineCamera(_levelDataProvider.PlayerStart,_gameplayConstants.cinemachineCameraPrefab);
        }
    }
}