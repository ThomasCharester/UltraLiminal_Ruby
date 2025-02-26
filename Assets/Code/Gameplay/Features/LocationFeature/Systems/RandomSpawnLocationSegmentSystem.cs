using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Pooler;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class RandomSpawnLocationSegmentSystem : IExecuteSystem
    {
        private readonly ILocationSegmentPoolerService _locationSegmentPoolerService;
        private readonly IGroup<GameEntity> _themOnTheBall;
        private List<GameEntity> buffer = new(8);

        public RandomSpawnLocationSegmentSystem(GameContext game, ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _themOnTheBall = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GotOnTheBall,
                GameMatcher.Transform
            ));
        }

        public void Execute()
        {
            foreach (var heOnTheBall in _themOnTheBall.GetEntities(buffer))
            {
                // TODO вынести рандом
                LocationSegmentID segmentID =
                    (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max() + 1);
                
                Vector3 segmentOriginPosition = heOnTheBall.Transform.position;

                GameEntity locationSegment = _locationSegmentPoolerService.GetPool(segmentID).Get();

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
                else if(segmentOriginYRotation < -305f) segmentOriginYRotation += 720f;

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