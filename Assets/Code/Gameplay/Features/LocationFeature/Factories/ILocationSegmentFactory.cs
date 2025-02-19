using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public interface ILocationSegmentFactory
    {
        GameEntity CreateLocationSegment(LocationSegmentID segmentID, Vector3 position, Quaternion rotation);
        GameEntity CreateRandomLocationSegment(Vector3 position, Quaternion rotation);
        void SpawnDoors(Transform segmentOrigin, DoorCalculator locationSegment, int id, int exceptionOriginIdInList = -1);

        void SpawnDoors(Vector3 segmentOriginPosition, Quaternion segmentOriginRotation, DoorCalculator locationSegment,
            int id,
            int exceptionOriginIdInList = -1);
        //void SpawnDoors(DoorCalculator locationSegment, int id, int exceptionOriginIdInList = -1);
    }
}