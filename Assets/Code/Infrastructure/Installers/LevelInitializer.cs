using Code.Gameplay.Features.Items;
using Code.Gameplay.Level;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        public Transform PlayerStart;
        public InventoryID LevelInventoryID;
        private ILevelDataProvider _levelDataProvider;
        [Inject]
        public void Construct(ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }
        
        public void Initialize()
        {
            _levelDataProvider.SetPlayerStart(PlayerStart.position);
            _levelDataProvider.SetLevelInventoryID(LevelInventoryID);
        }
    }
}