using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Common.Pooler;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public class DoorFactory : IDoorFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;
        private readonly IDoorPoolerService _doorPoolerService;

        public DoorFactory(IIdentifierService identifierService, IStaticDataService staticDataService,
            IDoorPoolerService doorPoolerService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
            _doorPoolerService = doorPoolerService;
        }

        public GameEntity CreateDoor(DoorID segmentID, in Vector3 originPosition, in Quaternion originRotation,
            int ownerID) // Need some ref
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddOwnerFrame(ownerID)
                .AddHingeJointAnchorPosition(originPosition)
                .AddHingeJointAnchorRotation(originRotation)
                .AddViewPrefab(_staticDataService.GetDoorConfig(segmentID).doorPrefab)
                .With(x => x.isDoorOff = true)
                .With(x => x.isActiveOnScene = true);
        }

        public GameEntity
            CreateDoorFrame(in Vector3 originPosition, in Quaternion originRotation, int masterID) // Need some ref
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddMasterLocationSegment(masterID)
                .AddVectorSpawnPoint(originPosition)
                .AddRotationSpawnPoint(originRotation)
                .AddViewPrefab(_staticDataService.GetDoorConfig(DoorID.DoorFrame).doorPrefab)
                .With(x => x.isActiveOnScene = true);
        }

        public void SpawnDoors(in Vector3 segmentOriginPosition, in Quaternion segmentOriginRotation,
            in DoorCalculator locationSegment, int segmentID,
            int exceptionOriginIdInList = -1) // Need some ref
        {
            foreach (var doorOrigin in locationSegment.GetDoorOrigins)
            {
                if (exceptionOriginIdInList > -1 &&
                    doorOrigin == locationSegment.GetDoorOrigins[exceptionOriginIdInList]) continue;

                float doorOriginRotation = segmentOriginRotation.eulerAngles.y + doorOrigin.rotation.eulerAngles.y;
                if (doorOriginRotation > 305f) doorOriginRotation -= 360f;
                else if (doorOriginRotation < -45f) doorOriginRotation += 360f;

                Vector3 trueDoorOrigin = segmentOriginPosition;
                Quaternion trueDoorRotation = Quaternion.Euler(0, doorOriginRotation, 0);

                float trueSegmentRotation = segmentOriginRotation.eulerAngles.y;
                if (trueSegmentRotation > 305f) trueSegmentRotation -= 360f;
                else if (trueSegmentRotation < -45f) trueSegmentRotation += 360f;

                
                // Как бы заменить на перемножение синусов и косинусов
                if (trueSegmentRotation is > -45f and < 45f)
                {
                    trueDoorOrigin.x += doorOrigin.position.x;
                    trueDoorOrigin.z += doorOrigin.position.z;
                    trueDoorOrigin.x += _staticDataService.GameplayConstantsConfig._doorOffset; // Настроить
                }
                else if (trueSegmentRotation is >= 45f and < 135f)
                {
                    trueDoorOrigin.x += doorOrigin.position.z;
                    trueDoorOrigin.z -= doorOrigin.position.x;
                    trueDoorOrigin.z += _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (trueSegmentRotation is >= 135f and < 225f)
                {
                    trueDoorOrigin.x -= doorOrigin.position.x;
                    trueDoorOrigin.z -= doorOrigin.position.z;
                    trueDoorOrigin.x -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }
                else if (trueSegmentRotation is >= 225f and < 305f)
                {
                    trueDoorOrigin.x -= doorOrigin.position.z;
                    trueDoorOrigin.z += doorOrigin.position.x;
                    trueDoorOrigin.z -= _staticDataService.GameplayConstantsConfig._doorOffset;
                }

                trueDoorOrigin.y += doorOrigin.localPosition.y;

                GameEntity frame = _doorPoolerService.GetPool(DoorID.DoorFrame).Get();
                frame.AddMasterLocationSegment(segmentID)
                    .AddVectorSpawnPoint(trueDoorOrigin)
                    .AddRotationSpawnPoint(trueDoorRotation);

                trueDoorOrigin.y += _staticDataService.GameplayConstantsConfig._doorFrameVerticalOffset;

                GameEntity door = _doorPoolerService
                    .GetPool((DoorID)Random.Range(1, Enum.GetValues(typeof(DoorID)).Length)).Get();
                
                door.AddOwnerFrame(frame.Id)
                    .AddHingeJointAnchorPosition(trueDoorOrigin)
                    .AddHingeJointAnchorRotation(trueDoorRotation);
            }
        }
    }
}