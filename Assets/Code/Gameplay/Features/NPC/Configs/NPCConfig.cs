using Code.Gameplay.Features.Items;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.NPC.Configs
{
    [CreateAssetMenu(menuName = "NPC", fileName = "NPCConfig")]
    public class NPCConfig : ScriptableObject
    {
        public NPCID npcId;
        public string npcName;
        public Transform transform;
        public EntityBehaviour npcPrefab;
        public float watchRadius;
        public bool watchingForTargets;
    }
}