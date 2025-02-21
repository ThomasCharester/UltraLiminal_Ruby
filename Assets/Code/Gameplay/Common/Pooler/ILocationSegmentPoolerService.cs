using System.Collections.Generic;
using Code.Gameplay.Features.LocationFeature;

namespace Code.Gameplay.Common.Pooler
{
    public interface ILocationSegmentPoolerService
    {
        Dictionary<LocationSegmentID, LocationSegmentPool> SegmentPools { get; set; }
    }
}