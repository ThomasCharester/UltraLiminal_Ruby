using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.AnimationRigShit;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ObjectSeek.Systems
{
    public class WatchForTargetsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> seekers;
        private readonly IGroup<GameEntity> targets;
        private List<GameEntity> _bufferTargets = new(32);
        private List<GameEntity> _bufferSeekers = new(32);

        public WatchForTargetsSystem(GameContext game)
        {
            seekers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WatchingForTargets, 
                GameMatcher.Transform));
            targets = game.GetGroup(GameMatcher.TrackingTarget);
        }

        public void Execute()
        {
            targets.GetEntities(_bufferTargets);
            
            if (_bufferTargets.Count == 0) return;
            
            foreach (var seeker in seekers.GetEntities(_bufferSeekers))
            {
                Transform trackingTarget = _bufferTargets.OrderBy(n => Vector3.Distance(n.TrackingTarget.position, seeker.Transform.position))
                    .First().TrackingTarget;
                
                if (seeker.hasWatchRadius && Vector3.Distance(trackingTarget.position, seeker.Transform.position) < seeker.WatchRadius)
                    seeker.ReplaceFoundTarget(trackingTarget);
                else if (seeker.hasWatchRadius && seeker.hasFoundTarget) seeker.RemoveFoundTarget();
            }
        }
    }
}