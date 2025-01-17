using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IIdentifierService _identifierService;

        public PlayerFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreatePlayer(Vector3 position)
        {
            return CreateEntity.Empty()
                    .AddId(_identifierService.NextId())
                    .AddVectorSpawnPoint(position)
                    .AddSpeed(5f)
                    .AddViewPath("Prefabs/Player/Player")
                    .With(x => x.isPlayer = true);
        }
    }
}