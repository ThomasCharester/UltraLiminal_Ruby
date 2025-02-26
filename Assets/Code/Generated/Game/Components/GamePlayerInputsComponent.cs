//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPlayerInputs;

    public static Entitas.IMatcher<GameEntity> PlayerInputs {
        get {
            if (_matcherPlayerInputs == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerInputs);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerInputs = matcher;
            }

            return _matcherPlayerInputs;
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

    public Code.Gameplay.Input.PlayerInputs playerInputs { get { return (Code.Gameplay.Input.PlayerInputs)GetComponent(GameComponentsLookup.PlayerInputs); } }
    public Code.Gameplay.Movement.Controller.PlayerCharacterInputs PlayerInputs { get { return playerInputs.Value; } }
    public bool hasPlayerInputs { get { return HasComponent(GameComponentsLookup.PlayerInputs); } }

    public GameEntity AddPlayerInputs(Code.Gameplay.Movement.Controller.PlayerCharacterInputs newValue) {
        var index = GameComponentsLookup.PlayerInputs;
        var component = (Code.Gameplay.Input.PlayerInputs)CreateComponent(index, typeof(Code.Gameplay.Input.PlayerInputs));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePlayerInputs(Code.Gameplay.Movement.Controller.PlayerCharacterInputs newValue) {
        var index = GameComponentsLookup.PlayerInputs;
        var component = (Code.Gameplay.Input.PlayerInputs)CreateComponent(index, typeof(Code.Gameplay.Input.PlayerInputs));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePlayerInputs() {
        RemoveComponent(GameComponentsLookup.PlayerInputs);
        return this;
    }
}
