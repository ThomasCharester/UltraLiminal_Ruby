using UnityEngine;

namespace Code.Gameplay.Features.NPC.Factories
{
    public interface INPCFactory
    {
        GameEntity CreateNPC(Vector3 spawnPoint, NPCID npcid);
    }
}