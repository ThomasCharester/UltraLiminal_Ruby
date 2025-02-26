//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherGotOnTheBall;

    public static Entitas.IMatcher<GameEntity> GotOnTheBall {
        get {
            if (_matcherGotOnTheBall == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GotOnTheBall);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGotOnTheBall = matcher;
            }

            return _matcherGotOnTheBall;
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

    static readonly Code.Gameplay.Features.LocationFeature.GotOnTheBall gotOnTheBallComponent = new Code.Gameplay.Features.LocationFeature.GotOnTheBall();

    public bool isGotOnTheBall {
        get { return HasComponent(GameComponentsLookup.GotOnTheBall); }
        set {
            if (value != isGotOnTheBall) {
                var index = GameComponentsLookup.GotOnTheBall;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : gotOnTheBallComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
