//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCrouchButton;

    public static Entitas.IMatcher<GameEntity> CrouchButton {
        get {
            if (_matcherCrouchButton == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CrouchButton);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCrouchButton = matcher;
            }

            return _matcherCrouchButton;
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

    public Code.Gameplay.Input.CrouchButton crouchButton { get { return (Code.Gameplay.Input.CrouchButton)GetComponent(GameComponentsLookup.CrouchButton); } }
    public bool CrouchButton { get { return crouchButton.Value; } }
    public bool hasCrouchButton { get { return HasComponent(GameComponentsLookup.CrouchButton); } }

    public GameEntity AddCrouchButton(bool newValue) {
        var index = GameComponentsLookup.CrouchButton;
        var component = (Code.Gameplay.Input.CrouchButton)CreateComponent(index, typeof(Code.Gameplay.Input.CrouchButton));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceCrouchButton(bool newValue) {
        var index = GameComponentsLookup.CrouchButton;
        var component = (Code.Gameplay.Input.CrouchButton)CreateComponent(index, typeof(Code.Gameplay.Input.CrouchButton));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveCrouchButton() {
        RemoveComponent(GameComponentsLookup.CrouchButton);
        return this;
    }
}
