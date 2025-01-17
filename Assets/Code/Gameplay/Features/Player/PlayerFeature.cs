using System;
using Code.Common.Entity;
using Code.Gameplay.Features.Player.Systems;
using Code.Gameplay.Movement.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Player
{
    public class PlayerFeature : Feature
    {
        public PlayerFeature(ISystemFactory systems)
        {
            Add(systems.Create<PlayerInitializeSystem>());
            Add(systems.Create<InitializePlayerInventorySystem>());
            
            Add(systems.Create<SetPlayerInputsSystem>());
            Add(systems.Create<SendPlayerInputsToControllerSystem>());
            Add(systems.Create<SetPlayerAnimationMovementSystem>());
            
            Add(systems.Create<ProcessPlayerUseSystem>());
        }
    }
}