namespace Code.Gameplay.Input.Service
{
    public interface IInputService
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
        bool HasAxisInput();
        bool GetCrouchButton();
        bool GetJumpButton();
        bool GetUseButton();
        bool GetActionButton(string actionName);
    }
}