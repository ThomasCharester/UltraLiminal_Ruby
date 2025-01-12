using UnityEngine;

namespace Code.Infrastructure.ShitManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadAsset(string assetName)
        {
            return Resources.Load<GameObject>(assetName);
        }

        public T LoadAsset<T>(string assetName) where T : Component
        {
            return Resources.Load<T>(assetName);
        }
    }
}