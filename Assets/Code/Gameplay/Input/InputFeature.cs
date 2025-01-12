using Code.Gameplay.Input.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeInputSystem>());
            Add(systems.Create<GetNSetAxisInput>());
            Add(systems.Create<GetNSetCrouchButton>());
            Add(systems.Create<GetNSetJumpButton>());
            Add(systems.Create<GetNSetUseButton>());
        }
    }
}