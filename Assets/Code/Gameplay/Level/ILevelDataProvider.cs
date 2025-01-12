using UnityEngine;

namespace Code.Gameplay.Level
{
    public interface ILevelDataProvider
    {
        Vector3 PlayerStart { get; }
        void SetPlayerStart(Vector3 playerStart);
    }
}