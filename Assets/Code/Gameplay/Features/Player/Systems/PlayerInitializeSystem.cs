using Code.Gameplay.Features.Player.Factory;
using Code.Gameplay.Level;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Systems
{
    public class PlayerInitializeSystem : IInitializeSystem
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly ILevelDataProvider _levelDataProvider;

        public PlayerInitializeSystem(IPlayerFactory playerFactory, ILevelDataProvider levelDataProvider)
        {
            _playerFactory = playerFactory;
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _playerFactory.CreatePlayer(_levelDataProvider.PlayerStart);
        }
    }
}