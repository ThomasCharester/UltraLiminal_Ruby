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
                Vector3 randomDoorOriginPosition = randomDoorOrigin.position;
                Quaternion randomDoorOriginRotation = randomDoorOrigin.rotation;
                
                // _locationSegmentFactory.SpawnDoors(locationSegment.LocationSegment, locationSegment.Id,
                //     locationSegment.LocationSegment.GetDoorOrigins.IndexOf(randomDoorOrigin));

                float segmentOriginYRotation = 360 - heOnTheBall.Transform.rotation.y - randomDoorOriginRotation.y;

                if (segmentOriginYRotation is >= 0f and < 90.0f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.x;
                    segmentOriginPosition.z += randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 90f and < 180.0f)
                {
                    segmentOriginPosition.x += randomDoorOriginPosition.x;
                    segmentOriginPosition.z -= randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 180f and < 270.0f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.x;
                    segmentOriginPosition.z -= randomDoorOriginPosition.z;
                }
                else if (segmentOriginYRotation is >= 270f and < 360.0f)
                {
                    segmentOriginPosition.x -= randomDoorOriginPosition.x;
                    segmentOriginPosition.z += randomDoorOriginPosition.z;
                }

                locationSegment.ReplaceVectorSpawnPoint(_staticDataService.GameplayConstantsConfig._farAway);//segmentOriginPosition);
                locationSegment.ReplaceRotationSpawnPoint(new Quaternion(0, segmentOriginYRotation, 0, 0));

                _game.GetEntityWithId(heOnTheBall.OwnerDoor).AddSlaveLocationSegment(locationSegment.Id);
                heOnTheBall.isGotOnTheBall = false;
            }
        }
    }
}