using UnityEngine;

namespace Code.Common.Extensions
{
    public enum CollisionLayer
    {
        Player = 7,
        Items = 6
    }
    public static class CollisionExtentions
    {
        public static bool Matches(this Collider collider, LayerMask layerMask) =>
            ((1 << collider.gameObject.layer) & layerMask) != 0;

        public static int AsMask(this CollisionLayer layer) =>
            1 << (int)layer;
    }
}