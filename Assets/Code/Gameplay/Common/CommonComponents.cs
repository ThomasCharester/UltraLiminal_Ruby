using Code.Gameplay.Movement.Controller;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Common
{
    [Game] public class WorldPosition : IComponent { public Vector3 Value; } //
    [Game] public class Speed : IComponent { public float Value; } //
    [Game] public class WorldRotation : IComponent { public Quaternion Value; }
    [Game] public class Direction : IComponent { public Vector3 Value; }
    [Game] public class TransformComponent : IComponent { public Transform Value; }
    [Game] public class CharacterControllerComponent : IComponent { public IStandaloneCharacterController Value; }
    [Game] public class SpawnPoint : IComponent { public Vector3 Value; }
}