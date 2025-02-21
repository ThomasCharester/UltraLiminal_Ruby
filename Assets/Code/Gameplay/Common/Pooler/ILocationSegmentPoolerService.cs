using Code.Gameplay.Features.LocationFeature;
using UnityEngine.Pool;

namespace Code.Gameplay.Common.Pooler
{
    public interface ILocationSegmentPoolerService
    {
        public IObjectPool<GameEntity> GetPool(LocationSegmentID locationSegmentID);
    }
}