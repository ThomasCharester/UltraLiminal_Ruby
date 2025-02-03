using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.AnimationRigShit.Systems
{
    public class DecreaseWieghtOnTergetSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> seekers;
        private List<GameEntity> _bufferSeekers = new(32);

        public DecreaseWieghtOnTergetSystem(GameContext game, ITimeService time)
        {
            _time = time;
            seekers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.MultiAimConstraint,
                GameMatcher.RigBuilder,
                GameMatcher.NotYetUnWeighted));
        }

        public void Execute()
        {
            foreach (var seeker in seekers.GetEntities(_bufferSeekers))
            {
                var data = seeker.MultiAimConstraint.data.sourceObjects;
                data.SetWeight(0, data.GetWeight(0) - _time.DeltaTime);

                if (data.GetWeight(0) <= 0f)
                {
                    seeker.isNotYetUnWeighted = false;

                    data.SetTransform(0, null);
                }

                seeker.MultiAimConstraint.data.sourceObjects = data;

                seeker.RigBuilder.Build();
            }
        }
    }
}