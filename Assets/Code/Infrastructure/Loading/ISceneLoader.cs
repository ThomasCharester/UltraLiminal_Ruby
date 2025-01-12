using System;

namespace Code.Infrastructure.Loading
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}