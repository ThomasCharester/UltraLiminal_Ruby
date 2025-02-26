//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherLowerStairwellID;

    public static Entitas.IMatcher<GameEntity> LowerStairwellID {
        get {
            if (_matcherLowerStairwellID == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LowerStairwellID);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLowerStairwellID = matcher;
            }

            return _matcherLowerStairwellID;
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

    public Code.Gameplay.Features.LocationFeature.LowerStairwellID lowerStairwellID { get { return (Code.Gameplay.Features.LocationFeature.LowerStairwellID)GetComponent(GameComponentsLookup.LowerStairwellID); } }
    public int LowerStairwellID { get { return lowerStairwellID.Value; } }
    public bool hasLowerStairwellID { get { return HasComponent(GameComponentsLookup.LowerStairwellID); } }

    public GameEntity AddLowerStairwellID(int newValue) {
        var index = GameComponentsLookup.LowerStairwellID;
        var component = (Code.Gameplay.Features.LocationFeature.LowerStairwellID)CreateComponent(index, typeof(Code.Gameplay.Features.LocationFeature.LowerStairwellID));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceLowerStairwellID(int newValue) {
        var index = GameComponentsLookup.LowerStairwellID;
        var component = (Code.Gameplay.Features.LocationFeature.LowerStairwellID)CreateComponent(index, typeof(Code.Gameplay.Features.LocationFeature.LowerStairwellID));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveLowerStairwellID() {
        RemoveComponent(GameComponentsLookup.LowerStairwellID);
        return this;
    }
}
