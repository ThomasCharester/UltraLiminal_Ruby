using Code.Gameplay.Features.NPC;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Configs
{
    [CreateAssetMenu(menuName = "Player", fileName = "PlayerConfig")]
    public class PlayerConfig: ScriptableObject
    {
        public string playerName;
        public Transform spawnPoint;
        public EntityBehaviour playerPrefab;
        public float animationSpeed;
    }
}