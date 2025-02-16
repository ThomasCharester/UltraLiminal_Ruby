using System;
using Code.Infrastructure.View.Registrar;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Camera.Registrars
{
    public class CinemachineCameraRegistrar : EntityComponentRegistrar
    {
        public CinemachineCamera cinemachineCamera;
        public override void RegisterComponents()
        {
            Entity.AddCinemachineCamera(cinemachineCamera);
            Entity.isCinemachine = true;
        }

        public override void UnregisterComponents()
        {
            Entity.RemoveCinemachineCamera();
            Entity.isCinemachine = false;
        }
    }
}