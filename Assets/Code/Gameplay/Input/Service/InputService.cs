using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Input.Service
{
    public class InputService : IInputService
    {
        private readonly InputActionAsset _gameInput;
        private readonly InputAction _movement;
        private readonly InputAction _crouch;
        private readonly InputAction _jump;
        private readonly InputAction _use;
        

        public InputService(InputActionAsset gameInput)
        {
            _gameInput = gameInput;
            gameInput.FindActionMap("Game").Enable();
            _movement = gameInput.FindActionMap("Game").FindAction("Movement");
            _crouch = gameInput.FindActionMap("Game").FindAction("Crouch");
            _jump = gameInput.FindActionMap("Game").FindAction("Jump");
            _use = gameInput.FindActionMap("Game").FindAction("Use");
        }

        public float GetHorizontalAxis() => _movement.ReadValue<Vector2>().x;
        public float GetVerticalAxis() => _movement.ReadValue<Vector2>().y;
        public bool GetCrouchButton() => _crouch.ReadValue<float>() > 0;
        public bool GetJumpButton() => _jump.ReadValue<float>() > 0;
        public bool GetUseButton() => _use.ReadValue<float>() > 0;
        public bool HasAxisInput() => _movement.ReadValue<Vector2>() != Vector2.zero;
        
        public bool GetActionButton(string actionName) => _gameInput.FindAction(actionName).ReadValue<bool>();// Looks pretty shitty
    }
}