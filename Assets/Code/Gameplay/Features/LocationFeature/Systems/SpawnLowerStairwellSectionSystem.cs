using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class SpawnLowerStairwellSectionSystem : IExecuteSystem
    {
        private readonly ILocationSegmentFactory _locationSegmentFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _stairwellSections;
        private List<GameEntity> buff = new(8);

        public SpawnLowerStairwellSectionSystem(GameContext game, ILocationSegmentFactory locationSegmentFactory,
            IStaticDataService staticDataService)
        {
            _locationSegmentFactory = locationSegmentFactory;
            _staticDataService = staticDataService;
            _stairwellSections = game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.MultipleTriggerEventService,
                    GameMatcher.Stairwell,
                    GameMatcher.Transform)
                .NoneOf(GameMatcher.LowerStairwellID));
        }

        public void Execute()
        {
            foreach (var section in _stairwellSections.GetEntities(buff))
            {
                if (!section.MultipleTriggerEventService.GetEnteredEntities((int)StairwellColliderType.Middle)
                        .Any(x => x.isPlayer)) continue;

                Vector3 lowerSectionOrigin = section.Transform.position;
                lowerSectionOrigin.y -= _staticDataService.GameplayConstantsConfig._stairwellSectionVerticalOffset;
                
                var lowerSection = _locationSegmentFactory.CreateLocationSegment(LocationSegmentID.Stairwell,
                    lowerSectionOrigin, section.Transform.rotation);

                _locationSegmentFactory.SpawnDoors(lowerSectionOrigin, section.Transform.rotation, lowerSection.LocationSegment, lowerSection.Id);
                
                section.AddLowerStairwellID(lowerSection.Id);

                lowerSection.AddUpperStairwellID(section.Id);
            }
        }
    }
}