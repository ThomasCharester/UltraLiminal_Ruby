using System;

namespace Code.Gameplay.Common.Time
{
    public class TimeService : ITimeService
    {
        private float _globalTimeModifier = 1f;
        public float GlobalTimeModifier => _globalTimeModifier;
        
        public float DeltaTime => _globalTimeModifier > 0 ? UnityEngine.Time.deltaTime * _globalTimeModifier : 0f;
        
        public DateTime UtcNow => DateTime.UtcNow;
        
        public void BEHOLDTHEWORLD() => _globalTimeModifier = 0f;
        public void StopTime() => _globalTimeModifier = 0f;
        public void StartTime() => _globalTimeModifier = 1f;
        
        public void ManipulateTime(float value) => _globalTimeModifier = value;
    }
}