using System;

namespace Code.Gameplay.Common.Time
{
    public interface ITimeService
    {
        float GlobalTimeModifier { get; }
        float DeltaTime { get; }
        DateTime UtcNow { get; }
        void BEHOLDTHEWORLD();
        void StopTime();
        void StartTime();
        void ManipulateTime(float value);
    }
}