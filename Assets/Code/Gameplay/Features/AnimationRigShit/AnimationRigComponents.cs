using Entitas;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Gameplay.Features.AnimationRigShit
{
    [Game] public class MultiAimConstraintComponent : IComponent { public MultiAimConstraint Value; }
    [Game] public class TrackingTarget : IComponent { public Transform Value; }
    [Game] public class RigBuilderComponent : IComponent { public RigBuilder Value; }
    [Game] public class Weighted : IComponent { }
    [Game] public class NotYetWeighted : IComponent { }
    [Game] public class NotYetUnWeighted : IComponent { }
}