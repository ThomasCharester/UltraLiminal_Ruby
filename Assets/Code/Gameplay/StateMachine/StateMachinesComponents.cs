using System.Collections.Generic;
using Code.Gameplay.Features.DoorInteractionFeature;
using Entitas;

namespace Code.Gameplay.StateMachine
{
    [Game] public class StateManagers : IComponent { public List<IStateManager> Value; }
    [Game] public class DoorInteractionStateManagerComponent : IComponent { public DoorInteractionStateMachine Value; }
}