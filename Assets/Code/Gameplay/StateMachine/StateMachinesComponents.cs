using System.Collections.Generic;
using Code.Gameplay.Features.DoorInteractionFeature;
using Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine;
using Entitas;

namespace Code.Gameplay.StateMachine
{
    [Game] public class StateManagers : IComponent { public List<IStateManager> Value; }
    [Game] public class EnvironmentInteractionStateManagerComponent : IComponent { public EnvironmentInteractionStateMachine Value; }
    [Game] public class DoorInteractionStateManagerComponent : IComponent { public DoorInteractionStateMachine Value; }
}