using Code.Gameplay.Input.Service;
using Entitas;

namespace Code.Gameplay.Input.Systems
{
    public class GetNSetJumpButton : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<GameEntity> _inputs;

        public GetNSetJumpButton(GameContext game, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = game.GetGroup(GameMatcher.Input);
        }

        public void Execute()
        {
            foreach (var input in _inputs)
            {
                input.ReplaceJumpButton(_inputService.GetJumpButton());
                if (input.JumpButton && !input.isJumpButtonPressed && !input.isJumpButtonHold)
                {
                    input.isJumpButtonHold = true;
                    input.isJumpButtonPressed = true;
                }
                else if (!input.JumpButton && input.isJumpButtonPressed && input.isJumpButtonHold)
                {
                    input.isJumpButtonHold = false;
                }
                else if (input.JumpButton && input.isJumpButtonPressed && !input.isJumpButtonHold)
                {
                    input.isJumpButtonHold = true;
                    input.isJumpButtonPressed = false;
                }
                else if (!input.JumpButton && !input.isJumpButtonPressed && input.isJumpButtonHold)
                {
                    input.isJumpButtonHold = false;
                }
            }
        }
    }
}