using System.Collections.Generic;
using Code.Gameplay.Level;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelItemsInitializer : MonoBehaviour, IInitializable
    {
        public List<Transform> itemsSpawnPoints; //
        private ILevelDataProvider _levelDataProvider;
        [Inject]
        public void Construct(ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }
        
        public void Initialize()
        {
            _levelDataProvider.ItemSpawnPoints = itemsSpawnPoints;
        }
    }
}