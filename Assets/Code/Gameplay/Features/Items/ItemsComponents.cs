using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Items
{
    
    [Game] public class Item : IComponent { }
    [Game] public class Triggered : IComponent { }
    [Game] public class Inventory : IComponent { }
    [Game] public class ItemIDComponent : IComponent { public ItemID Value; }
    [Game] public class WorldItemList : IComponent { public List<ItemID> Value; }
    [Game] public class PlayerItemList : IComponent { public List<ItemID> Value; }
    
    
    
    [Game] public class Useless : IComponent { }
    [Game] public class DebugKeyMiniGameActivator : IComponent { }
}