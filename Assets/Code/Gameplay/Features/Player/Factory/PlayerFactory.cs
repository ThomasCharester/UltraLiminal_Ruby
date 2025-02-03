using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;
        
        public PlayerFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreatePlayer(Vector3 position)
        {
            return CreateEntity.Empty()
                    .AddId(_identifierService.NextId())
                    .AddVectorSpawnPoint(position == Vector3.zero ? _staticDataService.PlayerConfig.spawnPoint.position : position)
                    .AddViewPrefab(_staticDataService.PlayerConfig.playerPrefab)
                    .AddAnimationSpeed(_staticDataService.PlayerConfig.animationSpeed)
                    .With(x => x.isPlayer = true);
        }
    }
}