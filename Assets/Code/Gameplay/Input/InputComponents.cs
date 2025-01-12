using Code.Gameplay.Movement.Controller;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input
{
    [Game] public class Input : IComponent {}
    [Game] public class AxisInput : IComponent { public Vector2 Value; }
    [Game] public class CrouchButton : IComponent { public bool Value; }
    [Game] public class CrouchButtonPressed : IComponent { }
    [Game] public class CrouchButtonHold : IComponent { }
    [Game] public class JumpButton : IComponent { public bool Value; }
    [Game] public class JumpButtonPressed : IComponent { }
    [Game] public class JumpButtonHold : IComponent { }
    [Game] public class UseButton : IComponent { public bool Value; }
    [Game] public class UseButtonPressed : IComponent { }
    [Game] public class UseButtonHold : IComponent { }
    [Game] public class PlayerInputs : IComponent { public PlayerCharacterInputs Value; }
    
}