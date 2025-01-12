using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IIndentifierService _indentifierService;

        public PlayerFactory(IIndentifierService indentifierService)
        {
            _indentifierService = indentifierService;
        }

        public GameEntity CreatePlayer(Vector3 position)
        {
            return CreateEntity.Empty()
                    .AddId(_indentifierService.NextId())
                    .AddSpawnPoint(position)
                    .AddSpeed(5f)
                    .AddViewPath("Prefabs/Player/Player")
                    .With(x => x.isPlayer = true);
        }
    }
}