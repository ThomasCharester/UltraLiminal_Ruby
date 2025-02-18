using System;
using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public class LocationSegmentFactory : ILocationSegmentFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;
        private readonly IDoorFactory _doorFactory;

        public LocationSegmentFactory(IIdentifierService identifierService, IStaticDataService staticDataService,
            IDoorFactory doorFactory)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
            _doorFactory = doorFactory;
        }

        public GameEntity CreateLocationSegment(LocationSegmentID segmentID, Vector3 position, Quaternion rotation)
        {
            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddVectorSpawnPoint(position)
                .AddRotationSpawnPoint(rotation)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);

            return locationSegment;
        }

        public void SpawnDoors(Transform segmentOrigin, DoorCalculator locationSegment, int id,
            int exceptionOriginIdInList = -1)
        {
            foreach (var doorOrigin in locationSegment.GetDoorOrigins)
            {
                if (exceptionOriginIdInList > -1 &&
                    doorOrigin == locationSegment.GetDoorOrigins[exceptionOriginIdInList]) continue;

                doorOrigin.rotation = Quaternion.Euler(0,
                    segmentOrigin.rotation.eulerAngles.y + doorOrigin.localRotation.eulerAngles.y, 0);

                Vector3 trueDoorOrigin = segmentOrigin.position + doorOrigin.localPosition;

                if (doorOrigin.rotation.y is >= 0f and < 45f or >= 305f)
                {
                    trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (doorOrigin.rotation.y is >= 45f and < 135f)
                {
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (doorOrigin.rotation.y is >= 135f and < 225f)
                {
                    trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (doorOrigin.rotation.y is >= 225f and < 305f)
                {
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;
                }

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;
                _doorFactory.CreateDoor((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length),
                    trueDoorOrigin,
                    doorOrigin.rotation, id);

                Debug.Log("///////////////////////////////////////////////");
                Debug.Log("trueDoorOrigin " + trueDoorOrigin);
                Debug.Log("doorOrigin.rotation " + doorOrigin.rotation);
                Debug.Log("segmentOrigin.position " + segmentOrigin.position);
                Debug.Log("doorOrigin.localPosition " + doorOrigin.localPosition);
                Debug.Log("///////////////////////////////////////////////");
            }
        }

        public void SpawnDoors(DoorCalculator locationSegment, int id,
            int exceptionOriginIdInList = -1)
        {
            foreach (var doorOrigin in locationSegment.GetDoorOrigins)
            {
                if (exceptionOriginIdInList > -1 &&
                    doorOrigin == locationSegment.GetDoorOrigins[exceptionOriginIdInList]) continue;

                Vector3 trueDoorOrigin = doorOrigin.localPosition;

                if (doorOrigin.rotation.y is >= 0f and < 45f or >= 305f)
                {
                    trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (doorOrigin.rotation.y is >= 45f and < 135f)
                {
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (doorOrigin.rotation.y is >= 135f and < 225f)
                {
                    trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (doorOrigin.rotation.y is >= 225f and < 305f)
                {
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;
                }

                var frame = _doorFactory.CreateDoorFrame(trueDoorOrigin, doorOrigin.rotation, id);

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;

                _doorFactory.CreateDoor((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length),
                    trueDoorOrigin,
                    doorOrigin.rotation, frame.Id);

                // Debug.Log("///////////////////////////////////////////////");
                // Debug.Log("trueDoorOrigin " + trueDoorOrigin);
                // Debug.Log("doorOrigin.rotation " + doorOrigin.rotation);
                // Debug.Log("doorOrigin.localPosition " + doorOrigin.localPosition);
                // Debug.Log("///////////////////////////////////////////////");
            }
        }

        public GameEntity CreateRandomLocationSegment(Vector3 position, Quaternion rotation)
        {
            LocationSegmentID segmentID = LocationSegmentID.Tiny;
                //(LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max());

            var locationSegment = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddLocationSegment(_staticDataService.GetLocationSegmentConfig(segmentID).doorCalculator)
                .AddVectorSpawnPoint(position)
                .AddRotationSpawnPoint(rotation)
                .AddViewPrefab(_staticDataService.GetLocationSegmentConfig(segmentID).segmentPrefab);


            return locationSegment;
        }
    }
}