using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Configs
{
    [CreateAssetMenu(menuName = "Environment/LocationSegment", fileName = "locationSegmentConfig")]
    public class LocationSegmentConfig: ScriptableObject
    {
        public LocationSegmentID segmentID;
        public DoorCalculator doorCalculator;
        public EntityBehaviour segmentPrefab;
    }
}