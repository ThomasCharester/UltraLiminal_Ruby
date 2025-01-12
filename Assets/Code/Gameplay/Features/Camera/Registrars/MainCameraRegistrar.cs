using Code.Infrastructure.View.Registrar;
using Unity.Cinemachine;

namespace Code.Gameplay.Features.Camera.Registrars
{
    public class MainCameraRegistrar: EntityComponentRegistrar
    {
        public UnityEngine.Camera camera;
        public override void RegisterComponents()
        {
            Entity.AddMainCamera(camera);
        }

        public override void UnregisterComponents()
        {
            Entity.RemoveMainCamera();
        }
    }
}