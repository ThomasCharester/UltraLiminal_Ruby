using Code.Gameplay.Features.LocationFeature;
using UnityEngine.Pool;

namespace Code.Gameplay.Common.Pooler
{
    public interface IDoorPoolerService
    {
        IObjectPool<GameEntity> GetPool(DoorID doorID);
    }
}