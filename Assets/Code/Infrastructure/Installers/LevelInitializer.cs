using Code.Gameplay.Level;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        //public Camera MainCamera;
        public Transform PlayerStart;
        private ILevelDataProvider _levelDataProvider;
        [Inject]
        public void Construct(ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }
        
        public void Initialize()
        {
            _levelDataProvider.SetPlayerStart(PlayerStart.position);
        }
    }
}