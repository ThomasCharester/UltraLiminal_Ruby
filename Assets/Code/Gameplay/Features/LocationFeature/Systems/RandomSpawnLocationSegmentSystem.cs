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

                float segmentOriginYRotation;

                float heOnTheBallRotation = heOnTheBall.Transform.rotation.eulerAngles.y;
                if (heOnTheBallRotation > 305f) heOnTheBallRotation -= 360f;
                else if (heOnTheBallRotation < -45f) heOnTheBallRotation += 360f;

                if (heOnTheBallRotation > 135f) 
                    segmentOriginYRotation = heOnTheBallRotation - randomDoorOriginRotation.eulerAngles.y + 180f;
                else
                    segmentOriginYRotation = heOnTheBallRotation - randomDoorOriginRotation.eulerAngles.y - 180f;
                
                if (segmentOriginYRotation < -45f) segmentOriginYRotation += 360f;
                else if (segmentOriginYRotation > 305f) segmentOriginYRotation -= 360f;

                if (segmentOriginYRotation is > -45f and < 45f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.x;
                    segmentOriginPosition.z -= randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 45f and < 135f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.z;
                    segmentOriginPosition.z += randomDoorOriginPosition.x;
                }
                else if (segmentOriginYRotation is >= 135f and < 225f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.x;
                    segmentOriginPosition.z += randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 225f and < 305f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.z;
                    segmentOriginPosition.z -= randomDoorOriginPosition.x;
                }

                segmentOriginPosition.y -= randomDoorOriginPosition.y;

                // Debug.Log("///////////////////////////////////////////////");
                // Debug.Log("segmentOriginPosition " + segmentOriginPosition);
                // Debug.Log("randomDoorOriginPosition " + randomDoorOriginPosition);
                // Debug.Log("heOnTheBall.Transform.position " + heOnTheBall.Transform.position);
                // Debug.Log("===============================================");
                // Debug.Log("randomDoorOriginRotation " + randomDoorOriginRotation.eulerAngles.y);
                // Debug.Log("realOnTheBall " + heOnTheBallRotation);
                // Debug.Log("segmentOriginYRotation " + segmentOriginYRotation);
                // Debug.Log("///////////////////////////////////////////////");

                locationSegment.ReplaceVectorSpawnPoint(segmentOriginPosition);
                locationSegment.ReplaceRotationSpawnPoint(Quaternion.Euler(0, segmentOriginYRotation, 0));
                locationSegment.isNeedSomeDoors = true;
                locationSegment.AddBadDoorId(locationSegment.LocationSegment.GetDoorOrigins.IndexOf(randomDoorOrigin));

                float frameOnTheBallRotation = segmentOriginYRotation + randomDoorOriginRotation.eulerAngles.y;
                if (frameOnTheBallRotation > 305f) frameOnTheBallRotation -= 360f;
                else if (frameOnTheBallRotation < -45f) frameOnTheBallRotation += 360f;
                
                heOnTheBall.AddSlaveLocationSegment(locationSegment.Id);
                heOnTheBall.ReplaceSlaveSegmentDoorOriginYRotation(frameOnTheBallRotation);
                heOnTheBall.isGotOnTheBall = false;
            }
        }
    }
}