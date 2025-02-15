using Code.Gameplay.Common.Physics;
using Code.Gameplay.Movement.Controller;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Common
{
    [Game] public class Direction : IComponent { public Vector3 Value; }
    [Game] public class TransformComponent : IComponent { public Transform Value; }
    [Game] public class CharacterControllerComponent : IComponent { public IStandaloneCharacterController Value; }
    [Game] public class VectorSpawnPoint : IComponent { public Vector3 Value; }
    [Game] public class RotationSpawnPoint : IComponent { public Quaternion Value; }
    [Game] public class TransformSpawnPoint : IComponent { public Transform Value; }
    [Game] public class LayerMask : IComponent { public int Value; }
    [Game] public class AnimationSpeed : IComponent { public float Value; } 
    [Game] public class ColliderEventServiceComponent : IComponent { public IColliderEventService Value; } 
    
}