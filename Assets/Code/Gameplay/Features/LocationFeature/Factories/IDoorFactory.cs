using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public interface IDoorFactory
    {
        GameEntity CreateDoor(DoorID segmentID, in Vector3 originPosition, in Quaternion originRotation, int ownerID);
        GameEntity CreateDoorFrame(in Vector3 originPosition,in  Quaternion originRotation, int masterID);

        void SpawnDoors(in Vector3 segmentOriginPosition, in Quaternion segmentOriginRotation,in DoorCalculator locationSegment,
            int segmentID,
            int exceptionOriginIdInList = -1);
    }
}