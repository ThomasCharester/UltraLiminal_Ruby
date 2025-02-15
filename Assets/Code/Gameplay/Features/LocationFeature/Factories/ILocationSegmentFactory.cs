using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Factories
{
    public interface ILocationSegmentFactory
    {
        GameEntity CreateLocationSegment(LocationSegmentID segmentID, Vector3 position, Quaternion rotation);
    }
}