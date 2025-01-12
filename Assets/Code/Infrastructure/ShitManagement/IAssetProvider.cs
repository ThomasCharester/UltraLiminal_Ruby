using UnityEngine;

namespace Code.Infrastructure.ShitManagement
{
    public interface IAssetProvider
    {
        GameObject LoadAsset(string assetName);
        T LoadAsset<T>(string assetName) where T : Component;
    }
}