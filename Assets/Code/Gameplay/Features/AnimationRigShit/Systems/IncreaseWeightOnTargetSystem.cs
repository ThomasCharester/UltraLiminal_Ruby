using System;
using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.AnimationRigShit.Systems
{
    public class IncreaseWeightOnTargetSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> seekers;
        private List<GameEntity> _bufferSeekers = new(32);

        public IncreaseWeightOnTargetSystem(GameContext game, ITimeService time)
        {
            _time = time;
            seekers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.MultiAimConstraint,
                GameMatcher.RigBuilder,
                GameMatcher.NotYetWeighted,
                GameMatcher.FoundTarget));
        }

        public void Execute()
        {
            foreach (var seeker in seekers.GetEntities(_bufferSeekers))
            {
                var data = seeker.MultiAimConstraint.data.sourceObjects;
                data.SetWeight(0, data.GetWeight(0) + _time.DeltaTime);
                seeker.MultiAimConstraint.data.sourceObjects = data;

                if (data.GetWeight(0) >= 1.0f)
                {
                    seeker.isNotYetWeighted = false;
                    seeker.isWeighted = true;
                }

                seeker.RigBuilder.Build();
            }
        }
    }
}