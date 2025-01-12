using Code.Gameplay.Features.Camera;
using Code.Gameplay.Features.Player;
using Code.Gameplay.Input;
using Code.Gameplay.Movement;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
    public class MainGameplayFeature : Feature
    {
        public MainGameplayFeature(ISystemFactory systems)
        {
            Add(systems.Create<InputFeature>());
            Add(systems.Create<BindViewFeature>());
            Add(systems.Create<MovementFeature>());
            
            Add(systems.Create<PlayerFeature>());
            Add(systems.Create<CameraFeature>());
        }
    }
}