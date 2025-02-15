using System.Collections.Generic;
using Code.Gameplay.Features.Items;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Configs
{
    [CreateAssetMenu(menuName = "Environment/Location", fileName = "locationSegmentConfig")]
    public class LocationSegmentConfig: ScriptableObject
    {
        public LocationSegmentID segmentID;
        public DoorCalculator doorCalculator;
        public EntityBehaviour segmentPrefab;
    }
}