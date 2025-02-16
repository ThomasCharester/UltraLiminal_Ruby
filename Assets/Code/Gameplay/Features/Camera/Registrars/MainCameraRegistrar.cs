using Code.Infrastructure.View.Registrar;
using Unity.Cinemachine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Camera.Registrars
{
    public class MainCameraRegistrar: EntityComponentRegistrar
    {
        public UnityEngine.Camera mainCamera;
        public override void RegisterComponents()
        {
            Entity.AddMainCamera(mainCamera);
        }

        public override void UnregisterComponents()
        {
            Entity.RemoveMainCamera();
        }
    }
}