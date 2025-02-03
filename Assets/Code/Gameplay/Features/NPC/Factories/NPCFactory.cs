using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.NPC.Factories
{
    public class NPCFactory : INPCFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public NPCFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateNPC(Vector3 spawnPoint, NPCID npcid) // to do exceptions?
        {
            var newNpc = CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .AddVectorSpawnPoint(spawnPoint == Vector3.zero
                    ? _staticDataService.GetNPCConfig(npcid).transform.position
                    : spawnPoint)
                .AddViewPrefab(_staticDataService.GetNPCConfig(npcid).npcPrefab)
                .AddWatchRadius(_staticDataService.GetNPCConfig(npcid).watchRadius)
                .With(x => x.isNPC = true)
                .With(x => x.isWatchingForTargets = _staticDataService.GetNPCConfig(npcid).watchingForTargets);
            
            return newNpc;
        }
    }
}