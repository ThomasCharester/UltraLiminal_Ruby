using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public interface ILocationSegmentFactory
    {
        GameEntity CreateLocationSegment(LocationSegmentID segmentID, in Vector3 position, in Quaternion rotation);
        GameEntity CreateRandomLocationSegment(in Vector3 position, in Quaternion rotation);
    }
}