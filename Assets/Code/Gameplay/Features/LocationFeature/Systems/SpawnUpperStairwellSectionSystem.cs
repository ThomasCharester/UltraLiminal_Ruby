using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.StaticData;
using Entitas;
using ModestTree;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class SpawnUpperStairwellSectionSystem : IExecuteSystem
    {
        private readonly ILocationSegmentFactory _locationSegmentFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _stairwellSections;
        private List<GameEntity> buff = new(8);

        public SpawnUpperStairwellSectionSystem(GameContext game, ILocationSegmentFactory locationSegmentFactory, IStaticDataService staticDataService)
        {
            _locationSegmentFactory = locationSegmentFactory;
            _staticDataService = staticDataService;
            _stairwellSections = game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.MultipleTriggerEventService,
                    GameMatcher.Stairwell,
                    GameMatcher.Transform)
                .NoneOf(GameMatcher.UpperStairwellID));
        }

        public void Execute()
        {
            foreach (var section in _stairwellSections.GetEntities(buff))
            {
                if (section.MultipleTriggerEventService.GetEnteredEntities((int)StairwellColliderType.Upper).IsEmpty() || 
                    !section.MultipleTriggerEventService.GetEnteredEntities((int)StairwellColliderType.Upper)
                        .Any(x => x.isPlayer)) continue;

                Vector3 upperSectionOrigin = section.Transform.position;
                upperSectionOrigin.y += _staticDataService.GameplayConstantsConfig._stairwellSectionVerticalOffset;
                
                var upperSection = _locationSegmentFactory.CreateLocationSegment(LocationSegmentID.Stairwell,
                    upperSectionOrigin, section.Transform.rotation);

                _locationSegmentFactory.SpawnDoors(upperSectionOrigin, section.Transform.rotation, upperSection.LocationSegment, upperSection.Id);
                
                section.AddUpperStairwellID(upperSection.Id);

                upperSection.AddLowerStairwellID(section.Id);
                
                section.MultipleTriggerEventService.GetEnteredEntities((int)StairwellColliderType.Upper).Clear();
                
                Debug.Log("Spawned upper segment " + upperSection.Id);
            }
        }
    }
}