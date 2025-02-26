//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherNotYetWeighted;

    public static Entitas.IMatcher<GameEntity> NotYetWeighted {
        get {
            if (_matcherNotYetWeighted == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NotYetWeighted);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNotYetWeighted = matcher;
            }

            return _matcherNotYetWeighted;
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

    static readonly Code.Gameplay.Features.AnimationRigShit.NotYetWeighted notYetWeightedComponent = new Code.Gameplay.Features.AnimationRigShit.NotYetWeighted();

    public bool isNotYetWeighted {
        get { return HasComponent(GameComponentsLookup.NotYetWeighted); }
        set {
            if (value != isNotYetWeighted) {
                var index = GameComponentsLookup.NotYetWeighted;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : notYetWeightedComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
