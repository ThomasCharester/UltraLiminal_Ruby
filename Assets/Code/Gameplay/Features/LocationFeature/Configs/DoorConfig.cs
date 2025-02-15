using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Configs
{
    [CreateAssetMenu(menuName = "Door", fileName = "doorConfig")]
    public class DoorConfig : ScriptableObject
    {
        public DoorID doorID;
        public EntityBehaviour doorPrefab;
    }
}