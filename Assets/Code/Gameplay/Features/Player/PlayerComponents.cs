using System.Numerics;
using Code.Gameplay.Features.Player.Animator;
using Entitas;

namespace Code.Gameplay.Features.Player
{
    [Game] public class Player : IComponent { }
    [Game] public class PlayerAnimatorComponent : IComponent { public PlayerAnimator Value; }
    [Game] public class TriggeredItem : IComponent { public GameEntity Value; }
}