using System;
using System.Linq;
using System.Text;
using Code.Common.Entity.ToStrings;
using Code.Common.Extensions;
using Code.Gameplay.Features.Items;
using Code.Gameplay.Features.LocationFeature;
using Code.Gameplay.Features.Player;
//using Code.Common.Extensions;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace
public sealed partial class GameEntity : INamedEntity
{
    private EntityPrinter _printer;

    public override string ToString()
    {
        if (_printer == null)
            _printer = new EntityPrinter(this);

        _printer.InvalidateCache();

        return _printer.BuildToString();
    }

    public string EntityName(IComponent[] components)
    {
        try
        {
            if (components.Length == 1)
                return components[0].GetType().Name;
            
            foreach (IComponent component in components)
            {
                switch (component.GetType().Name)
                {
                    case nameof(Player):
                        return PrintPlayer();
                    case nameof(Item):
                        return PrintItem();
                    case nameof(Inventory):
                        return PrintInventory();
                    case nameof(DoorIDComponent):
                        return PrintDoor();
                    case nameof(LocationSegment):
                        return PrintLocationSegment();
                }
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }

        return components.First().GetType().Name;
    }

    private string PrintDoor()
    {
        return new StringBuilder($"Door ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    }
    private string PrintLocationSegment()
    {
        return new StringBuilder($"LocationSegment ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    }
    private string PrintPlayer()
    {
        return new StringBuilder($"Player ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    }
    private string PrintItem()
    {
        return new StringBuilder($"Item ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    }
    private string PrintInventory()
    {
        return new StringBuilder($"Inventory ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    }
    private string PrintStairwell()
    {
        return new StringBuilder($"Stairwell ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    }
  
    public string BaseToString() => base.ToString();
}