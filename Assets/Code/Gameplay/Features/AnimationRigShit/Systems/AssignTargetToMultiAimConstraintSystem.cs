using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AnimationRigShit.Systems
{
    public class AssignTargetToMultiAimConstraintSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> seekers;
        private List<GameEntity> _bufferSeekers = new(32);

        public AssignTargetToMultiAimConstraintSystem(GameContext game)
        {
            seekers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.MultiAimConstraint,
                GameMatcher.RigBuilder));
        }

        public void Execute()
        {
            foreach (var seeker in seekers.GetEntities(_bufferSeekers))
            {
                if (seeker.hasFoundTarget) // Спорная ситуация, которую можно решить условиями с булями
                {
                    var data = seeker.MultiAimConstraint.data.sourceObjects;
                    data.SetTransform(0, seeker.FoundTarget);
                    seeker.MultiAimConstraint.data.sourceObjects = data;

                    if (!seeker.isWeighted && !seeker.isNotYetWeighted)
                    {
                        seeker.isWeighted = false;
                        seeker.isNotYetWeighted = true;
                        seeker.isNotYetUnWeighted = false;
                    }
                }
                else if (seeker.MultiAimConstraint.data.sourceObjects.GetTransform(0) != null && !seeker.isNotYetUnWeighted)
                {
                    seeker.isWeighted = false;
                    seeker.isNotYetWeighted = false;
                    seeker.isNotYetUnWeighted = true;
                }

                seeker.RigBuilder.Build();
            }
        }
    }
}