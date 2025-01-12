using UnityEngine;

namespace Code.Gameplay.Movement.Controller
{
    public interface IStandaloneCharacterController
    {
        void SetInputs(PlayerCharacterInputs inputs);
        public void SetPosition(Vector3 position);
    }
}