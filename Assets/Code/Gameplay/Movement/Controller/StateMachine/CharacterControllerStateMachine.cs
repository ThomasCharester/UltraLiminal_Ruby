using Code.Gameplay.StateMachine;
using UnityEngine;

namespace Code.Gameplay.Movement.Controller.StateMachine
{
    public class CharacterControllerStateMachine : StateManager<CharacterControllerStateMachine.ECharacterControllerStates>
    {
        [SerializeField] private StandaloneCharacterController _CC;
        public enum ECharacterControllerStates
        {
            Stand,
            Crouch,
            Air
        }

        private CharacterControllerContext _context;
        public override void InitializeStateMachine()
        {
            _context = new CharacterControllerContext(_CC);
        }
    }
}