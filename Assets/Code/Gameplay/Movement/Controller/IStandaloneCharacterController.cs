using UnityEngine;

namespace Code.Gameplay.Movement.Controller
{
    public interface IStandaloneCharacterController
    {
        public void SetInputs(PlayerCharacterInputs inputs);
        public void SetPosition(Vector3 position);
        public CapsuleCollider GetCapsuleCollider();
        public Rigidbody GetRigidbody();
    }
}