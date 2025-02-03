using System.Collections.Generic;
using Code.Gameplay.Features.NPC.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.NPC.Systems
{
    public class SpawnTestNPCSystem : IInitializeSystem
    {
        private readonly INPCFactory _npcFactory;

        public SpawnTestNPCSystem(INPCFactory npcFactory)
        {
            _npcFactory = npcFactory;
        }

        public void Initialize()
        {
            _npcFactory.CreateNPC(Vector3.zero, NPCID.TestNpc);
        }
    }
}