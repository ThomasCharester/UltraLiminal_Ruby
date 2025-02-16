using Code.Infrastructure.View;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Common
{
    [Game] public class Destructed : IComponent { }
    [Game] public class SelfDestructTimer : IComponent { public float Value; }

    [Game] public class View : IComponent { public IEntityView Value; }
    [Game] public class ViewPath : IComponent { public string Value; }
    [Game] public class ViewPrefab : IComponent { public EntityBehaviour Value; }
    
    [Game] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
}