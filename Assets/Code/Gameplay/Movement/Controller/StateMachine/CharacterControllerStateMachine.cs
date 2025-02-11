using Code.Gameplay.StateMachine;

namespace Code.Gameplay.Movement.Controller.StateMachine
{
    public class CharacterControllerStateMachine : StateManager<CharacterControllerStateMachine.ECharacterControllerStates>
    {
        public enum ECharacterControllerStates
        {
            Stand,
            Crouch,
            Air
        }

        private CharacterControllerContext _context;
        public override void InitializeStateMachine()
        {
            _context = new CharacterControllerContext();
        }
    }
}