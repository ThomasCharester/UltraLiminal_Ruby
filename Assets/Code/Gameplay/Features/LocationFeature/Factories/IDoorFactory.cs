using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public interface IDoorFactory
    {
        GameEntity CreateDoor(DoorID segmentID, Vector3 originPosition, Quaternion originRotation, int masterID);
    }
}