using Code.Gameplay.Common;
using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Camera.Factory;
using Code.Gameplay.Features.Player.Factory;
using Code.Gameplay.Input;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Level;
using Code.Infrastructure.Indentifiers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.ShitManagement;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Factory;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        public InputActionAsset InputActionAsset;
        public GameplayConstants gameplayConstants;
        public override void InstallBindings()
        {
            BindAssetManagementServices();
            
            BindInfrastructureServices();
            BindCommonServices();
            BindInputService();
            BindGameplayServices();
            
            BindContexts();
            BindSystemFactories();
        }

        private void BindGameplayServices()
        {
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
            Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
            Container.Bind<GameplayConstants>().FromInstance(gameplayConstants).AsSingle();
        }

        private void BindContexts()
        {
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
      
            Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
        }
        private void BindInfrastructureServices()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IIndentifierService>().To<IndentifierService>().AsSingle();
        }
        private void BindSystemFactories()
        {
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
            Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
        }
        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
        private void BindCommonServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
        }

        private void BindInputService()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle().WithArguments(InputActionAsset); 
        }
        public void Initialize()
        {
            Container.Resolve<ISceneLoader>().LoadScene("Chistilinsk");   
        }

    }
}