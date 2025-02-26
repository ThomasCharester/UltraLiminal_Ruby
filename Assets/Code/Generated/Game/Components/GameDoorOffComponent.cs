//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherDoorOff;

    public static Entitas.IMatcher<GameEntity> DoorOff {
        get {
            if (_matcherDoorOff == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DoorOff);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDoorOff = matcher;
            }

            return _matcherDoorOff;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Gameplay.Features.LocationFeature.DoorOff doorOffComponent = new Code.Gameplay.Features.LocationFeature.DoorOff();

    public bool isDoorOff {
        get { return HasComponent(GameComponentsLookup.DoorOff); }
        set {
            if (value != isDoorOff) {
                var index = GameComponentsLookup.DoorOff;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : doorOffComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
