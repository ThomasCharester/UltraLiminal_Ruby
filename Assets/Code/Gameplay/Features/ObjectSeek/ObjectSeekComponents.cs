using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ObjectSeek
{
    [Game] public class FoundTarget : IComponent { public Transform Value; }
    [Game] public class WatchRadius : IComponent { public float Value; }
    [Game] public class WatchingForTargets : IComponent {  }
}