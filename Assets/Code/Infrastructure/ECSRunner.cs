using Code.Gameplay;
using Code.Infrastructure.Systems;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class ECSRunner : MonoBehaviour
    {
        private ISystemFactory _systemFactory;
        private MainGameplayFeature _mainGameplayFeature;
        
        [Inject]
        private void Construct(ISystemFactory systemFactory)
        {
           _systemFactory = systemFactory; 
        }
        private void Start()
        {
            _mainGameplayFeature = _systemFactory.Create<MainGameplayFeature>();
            _mainGameplayFeature.Initialize();
        }
        private void Update()
        {
            _mainGameplayFeature.Execute();
            _mainGameplayFeature.Cleanup();
        }
        private void OnDestroy()
        {
            _mainGameplayFeature.TearDown();
        }
    }
}