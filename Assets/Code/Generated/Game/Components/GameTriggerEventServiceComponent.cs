//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTriggerEventService;

    public static Entitas.IMatcher<GameEntity> TriggerEventService {
        get {
            if (_matcherTriggerEventService == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TriggerEventService);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTriggerEventService = matcher;
            }

            return _matcherTriggerEventService;
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

    public Code.Gameplay.Common.TriggerEventServiceComponent triggerEventService { get { return (Code.Gameplay.Common.TriggerEventServiceComponent)GetComponent(GameComponentsLookup.TriggerEventService); } }
    public Code.Gameplay.Common.Physics.ITriggerEventService TriggerEventService { get { return triggerEventService.Value; } }
    public bool hasTriggerEventService { get { return HasComponent(GameComponentsLookup.TriggerEventService); } }

    public GameEntity AddTriggerEventService(Code.Gameplay.Common.Physics.ITriggerEventService newValue) {
        var index = GameComponentsLookup.TriggerEventService;
        var component = (Code.Gameplay.Common.TriggerEventServiceComponent)CreateComponent(index, typeof(Code.Gameplay.Common.TriggerEventServiceComponent));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceTriggerEventService(Code.Gameplay.Common.Physics.ITriggerEventService newValue) {
        var index = GameComponentsLookup.TriggerEventService;
        var component = (Code.Gameplay.Common.TriggerEventServiceComponent)CreateComponent(index, typeof(Code.Gameplay.Common.TriggerEventServiceComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveTriggerEventService() {
        RemoveComponent(GameComponentsLookup.TriggerEventService);
        return this;
    }
}
