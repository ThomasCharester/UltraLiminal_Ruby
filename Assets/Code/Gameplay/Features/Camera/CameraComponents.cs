using Entitas;
using Unity.Cinemachine;
using UnityEngine;

namespace Code.Gameplay.Features.Camera
{
    [Game] public class Camera : IComponent { }
    [Game] public class Cinemachine : IComponent { }
    [Game] public class Tracking : IComponent { }
    [Game] public class CinemachineCameraComponent : IComponent { public CinemachineCamera Value; }
    [Game] public class MainCamera : IComponent { public UnityEngine.Camera Value; }
    [Game] public class CameraTrackingTarget : IComponent { public Transform Value; }
    
}