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

                float doorOriginRotation = segmentOrigin.rotation.eulerAngles.y + doorOrigin.rotation.eulerAngles.y;
                if (doorOriginRotation > 305f) doorOriginRotation -= 360f;
                else if (doorOriginRotation < -45f) doorOriginRotation += 360f;
                
                Vector3 trueDoorOrigin = segmentOrigin.position;
                Quaternion trueDoorRotation = Quaternion.Euler(0, doorOriginRotation, 0);

                float trueSegmentRotation = segmentOrigin.rotation.eulerAngles.y;
                if (trueSegmentRotation > 305f) trueSegmentRotation -= 360f;
                else if (trueSegmentRotation < -45f) trueSegmentRotation += 360f;

                if (trueSegmentRotation is > -45f and < 45f)
                {
                    trueDoorOrigin.x += doorOrigin.position.x;
                    trueDoorOrigin.z += doorOrigin.position.z;
                }
                else if (trueSegmentRotation is >= 45f and < 135f)
                {
                    trueDoorOrigin.x += doorOrigin.position.z;
                    trueDoorOrigin.z -= doorOrigin.position.x;
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (trueSegmentRotation is >= 135f and < 225f)
                {
                    trueDoorOrigin.x -= doorOrigin.position.x;
                    trueDoorOrigin.z -= doorOrigin.position.z;
                }
                else if (trueSegmentRotation is >= 225f and < 305f)
                {
                    trueDoorOrigin.x -= doorOrigin.position.z;
                    trueDoorOrigin.z += doorOrigin.position.x;
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                trueDoorOrigin.y += doorOrigin.localPosition.y;
                
                var frame = _doorFactory.CreateDoorFrame(trueDoorOrigin, trueDoorRotation, id);

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;

                _doorFactory.CreateDoor((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length),
                    trueDoorOrigin,
                    trueDoorRotation, frame.Id);
                // Debug.Log("///////////////////////////////////////////////");
                // Debug.Log("trueDoorOrigin " + trueDoorOrigin);
                // Debug.Log("segmentOrigin.position " + segmentOrigin.position);
                // Debug.Log("doorOrigin.localPosition " + doorOrigin.localPosition);
                // Debug.Log("===============================================");
                // Debug.Log("trueRotation " + trueDoorRotation.eulerAngles.y);
                // Debug.Log("doorOrigin.rotation.y " + doorOrigin.rotation.eulerAngles.y);
                // Debug.Log("segmentOrigin.rotation " + trueSegmentRotation);
                // Debug.Log("///////////////////////////////////////////////");
            }
        }

        public GameEntity CreateRandomLocationSegment(Vector3 position, Quaternion rotation)
        {
            LocationSegmentID segmentID = (LocationSegmentID)Random.Range(0, Enum.GetValues(typeof(LocationSegmentID)).Cast<int>().Max());

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