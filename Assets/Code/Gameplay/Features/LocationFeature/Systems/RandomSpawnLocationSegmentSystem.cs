using System;
using System.Linq;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class RandomSpawnLocationSegmentSystem : IExecuteSystem
    {
        private readonly ILocationSegmentFactory _locationSegmentFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _themOnTheBall;

        public RandomSpawnLocationSegmentSystem(GameContext game, ILocationSegmentFactory locationSegmentFactory,
            IStaticDataService staticDataService)
        {
            _locationSegmentFactory = locationSegmentFactory;
            _staticDataService = staticDataService;
            _themOnTheBall = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GotOnTheBall,
                GameMatcher.Transform
            ));
        }

        public void Execute()
        {
            foreach (var heOnTheBall in _themOnTheBall)
            {
                LocationSegmentID segmentID =
                    (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Length - 1);
                Vector3 segmentOriginPosition = heOnTheBall.Transform.position;

                GameEntity locationSegment =
                    _locationSegmentFactory.CreateRandomLocationSegment(
                        _staticDataService.GameplayConstantsConfig._farAway, Quaternion.identity);

                Transform randomDoorOrigin = locationSegment.LocationSegment.GetRandomDoorOrigin;
                Vector3 randomDoorOriginPosition = randomDoorOrigin.position;
                Quaternion randomDoorOriginRotation = randomDoorOrigin.rotation;
                
                float segmentOriginYRotation = 360 - heOnTheBall.Transform.rotation.y - randomDoorOriginRotation.y;

                if (segmentOriginYRotation is >= 0f and < 90.0f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.x;
                    segmentOriginPosition.z += randomDoorOriginPosition.z;
                }
                else if(segmentOriginYRotation is >= 90f and < 180.0f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.x;
                    segmentOriginPosition.z -= randomDoorOriginPosition.z;
                }
                else if(segmentOriginYRotation is >= 180f and < 270.0f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.x;
                    segmentOriginPosition.z -= randomDoorOriginPosition.z;
                }
                else if(segmentOriginYRotation is >= 270f and < 360.0f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.x;
                    segmentOriginPosition.z += randomDoorOriginPosition.z;
                }

                locationSegment.ReplaceVectorSpawnPoint(segmentOriginPosition);
                locationSegment.ReplaceRotationSpawnPoint(new Quaternion(0,segmentOriginYRotation,0,0));

                heOnTheBall.isGotOnTheBall = false;
            }
        }
    }
}