//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCharacterController;

    public static Entitas.IMatcher<GameEntity> CharacterController {
        get {
            if (_matcherCharacterController == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterController);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterController = matcher;
            }

            return _matcherCharacterController;
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

    public Code.Gameplay.Common.CharacterControllerComponent characterController { get { return (Code.Gameplay.Common.CharacterControllerComponent)GetComponent(GameComponentsLookup.CharacterController); } }
    public Code.Gameplay.Movement.Controller.IStandaloneCharacterController CharacterController { get { return characterController.Value; } }
    public bool hasCharacterController { get { return HasComponent(GameComponentsLookup.CharacterController); } }

    public GameEntity AddCharacterController(Code.Gameplay.Movement.Controller.IStandaloneCharacterController newValue) {
        var index = GameComponentsLookup.CharacterController;
        var component = (Code.Gameplay.Common.CharacterControllerComponent)CreateComponent(index, typeof(Code.Gameplay.Common.CharacterControllerComponent));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceCharacterController(Code.Gameplay.Movement.Controller.IStandaloneCharacterController newValue) {
        var index = GameComponentsLookup.CharacterController;
        var component = (Code.Gameplay.Common.CharacterControllerComponent)CreateComponent(index, typeof(Code.Gameplay.Common.CharacterControllerComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveCharacterController() {
        RemoveComponent(GameComponentsLookup.CharacterController);
        return this;
    }
}
