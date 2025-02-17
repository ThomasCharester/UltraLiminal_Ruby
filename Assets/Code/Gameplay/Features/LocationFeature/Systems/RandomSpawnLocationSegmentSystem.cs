using System;
using System.Collections.Generic;
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
        private readonly GameContext _game;
        private readonly ILocationSegmentFactory _locationSegmentFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _themOnTheBall;
        private List<GameEntity> buffer = new(8);

        public RandomSpawnLocationSegmentSystem(GameContext game, ILocationSegmentFactory locationSegmentFactory,
            IStaticDataService staticDataService)
        {
            _game = game;
            _locationSegmentFactory = locationSegmentFactory;
            _staticDataService = staticDataService;
            _themOnTheBall = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GotOnTheBall,
                GameMatcher.Transform
            ));
        }

        public void Execute()
        {
            foreach (var heOnTheBall in _themOnTheBall.GetEntities(buffer))
            {
                LocationSegmentID segmentID =
                    (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max());
                Vector3 segmentOriginPosition = heOnTheBall.Transform.position;

                GameEntity locationSegment =
                    _locationSegmentFactory.CreateRandomLocationSegment(
                        _staticDataService.GameplayConstantsConfig._farAway, Quaternion.identity);
                
                Transform randomDoorOrigin = locationSegment.LocationSegment.GetRandomDoorOrigin;
                Vector3 randomDoorOriginPosition = randomDoorOrigin.localPosition;
                Quaternion randomDoorOriginRotation = randomDoorOrigin.localRotation;

                float segmentOriginYRotation = 0;

                float masterLocationRotation = _game.GetEntityWithId(_game.GetEntityWithId(heOnTheBall.OwnerDoor)
                    .MasterLocationSegment).Transform.rotation.eulerAngles.y;
                float onTheBallLocationRotation = heOnTheBall.Transform.rotation.eulerAngles.y;

                 segmentOriginYRotation = 180f - Mathf.Abs(masterLocationRotation - onTheBallLocationRotation)
                                               - randomDoorOriginRotation.eulerAngles.y;
                
                if (segmentOriginYRotation is >= 0f and < 45f or >= 305f or <= -305f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.x;
                    segmentOriginPosition.z -= randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 45f and < 135f or <= -225f and > -305f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.z;
                    segmentOriginPosition.z += randomDoorOriginPosition.x;
                }
                else if (segmentOriginYRotation is >= 135f and < 225f or <= -135f and > -225f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.x;
                    segmentOriginPosition.z += randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 225f and < 305f or <= -45f and > -135f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.z;
                    segmentOriginPosition.z -= randomDoorOriginPosition.x;
                }

                segmentOriginPosition.y -= randomDoorOriginPosition.y;

                // Debug.Log("///////////////////////////////////////////////");
                // Debug.Log("segmentOriginPosition " + segmentOriginPosition);
                // Debug.Log("randomDoorOriginPosition " + randomDoorOriginPosition);
                // Debug.Log("heOnTheBall.Transform.position " + heOnTheBall.Transform.position);
                // Debug.Log("randomDoorOriginRotation " + randomDoorOriginRotation.eulerAngles.y);
                // Debug.Log("heOnTheBall.Transform.rotation " + onTheBallLocationRotation);
                // Debug.Log("masterLocationSegment " + masterLocationRotation);
                // Debug.Log("segmentOriginYRotation " + segmentOriginYRotation);
                // Debug.Log("///////////////////////////////////////////////");

                locationSegment.ReplaceVectorSpawnPoint(segmentOriginPosition);
                locationSegment.ReplaceRotationSpawnPoint(Quaternion.Euler(0, segmentOriginYRotation, 0));
                locationSegment.isNeedSomeDoors = true;
                locationSegment.AddBadDoorId(locationSegment.LocationSegment.GetDoorOrigins.IndexOf(randomDoorOrigin));

                _game.GetEntityWithId(heOnTheBall.OwnerDoor).AddSlaveLocationSegment(locationSegment.Id);
                heOnTheBall.isGotOnTheBall = false;
            }
        }
    }
}