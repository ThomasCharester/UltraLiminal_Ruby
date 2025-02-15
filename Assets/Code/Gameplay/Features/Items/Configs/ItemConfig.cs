using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Items.Configs
{
    [CreateAssetMenu(menuName = "Items/Item", fileName = "itemConfig")]
    public class ItemConfig : ScriptableObject
    {
        public ItemID itemID;
        public string itemName;
        public Transform transform;
        public EntityBehaviour itemPrefab;
    }
}