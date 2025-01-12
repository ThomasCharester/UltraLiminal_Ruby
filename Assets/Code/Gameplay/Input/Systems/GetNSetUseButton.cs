using Code.Gameplay.Input.Service;
using Entitas;

namespace Code.Gameplay.Input.Systems
{
    public class GetNSetUseButton : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<GameEntity> _inputs;

        public GetNSetUseButton(GameContext game, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = game.GetGroup(GameMatcher.Input);
        }

        public void Execute()
        {
            foreach (var input in _inputs)
            {
                input.ReplaceUseButton(_inputService.GetUseButton());
                if (input.UseButton && !input.isUseButtonPressed && !input.isUseButtonHold)
                {
                    input.isUseButtonHold = true;
                    input.isUseButtonPressed = true;
                }
                else if (!input.UseButton && input.isUseButtonPressed && input.isUseButtonHold)
                {
                    input.isUseButtonHold = false;
                }
                else if (input.UseButton && input.isUseButtonPressed && !input.isUseButtonHold)
                {
                    input.isUseButtonHold = true;
                    input.isUseButtonPressed = false;
                }
                else if (!input.UseButton && !input.isUseButtonPressed && input.isUseButtonHold)
                {
                    input.isUseButtonHold = false;
                }
            }
        }
    }
}