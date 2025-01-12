using Code.Gameplay.Input.Service;
using Entitas;

namespace Code.Gameplay.Input.Systems
{
    public class GetNSetCrouchButton: IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<GameEntity> _inputs;

        public GetNSetCrouchButton(GameContext game, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = game.GetGroup(GameMatcher.Input);
        }
        public void Execute()
        {
            foreach (var input in _inputs)
            {
                input.ReplaceCrouchButton(_inputService.GetCrouchButton());
                if (input.CrouchButton && !input.isCrouchButtonPressed && !input.isCrouchButtonHold)
                {
                    input.isCrouchButtonHold = true;
                    input.isCrouchButtonPressed = true;
                }
                else if (!input.CrouchButton && input.isCrouchButtonPressed && input.isCrouchButtonHold)
                {
                    input.isCrouchButtonHold = false;
                }
                else if (input.CrouchButton && input.isCrouchButtonPressed && !input.isCrouchButtonHold)
                {
                    input.isCrouchButtonHold = true;
                    input.isCrouchButtonPressed = false;
                }
                else if (!input.CrouchButton && !input.isCrouchButtonPressed && input.isCrouchButtonHold)
                {
                    input.isCrouchButtonHold = false;
                }

            }
        }
    }
}