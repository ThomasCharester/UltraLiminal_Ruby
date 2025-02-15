using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature
{
    
    [Game] public class DoorOff : IComponent {} 
    [Game] public class LocationSegment : IComponent { public DoorCalculator Value;} 
    [Game] public class MasterLocationSegment : IComponent { public int Value; }
    [Game] public class SlaveLocationSegment : IComponent { public int Value; }
    [Game] public class HingeJointComponent : IComponent { public HingeJoint Value; } // is it needed?
}