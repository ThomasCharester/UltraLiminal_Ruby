using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Configs
{
    [CreateAssetMenu(menuName = "Environment/LocationCount", fileName = "locationSegmentCountInPoolConfig")]
    public class LocationSegmentsCountInPoolConfig: ScriptableObject
    {
        public List<int> SegmentCount;
    }
}