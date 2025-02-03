using Code.Gameplay.Features.AnimationRigShit.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.AnimationRigShit
{
    public class AnimationRigFeature : Feature
    {
        public AnimationRigFeature(ISystemFactory systems)
        {
            Add(systems.Create<AssignTargetToMultiAimConstraintSystem>());
            
            Add(systems.Create<IncreaseWeightOnTargetSystem>());
            Add(systems.Create<DecreaseWieghtOnTergetSystem>());
        }
    }
}