using System;
using Code.Infrastructure.View.Registrar;
using Unity.Cinemachine;
using UnityEngine;

namespace Code.Gameplay.Features.Camera.Registrars
{
    public class CinemachineCameraRegistrar : EntityComponentRegistrar
    {
        public CinemachineCamera camera;
        public override void RegisterComponents()
        {
            Entity.AddCinemachineCamera(camera);
        }

        public override void UnregisterComponents()
        {
            Entity.RemoveCinemachineCamera();
        }
    }
}