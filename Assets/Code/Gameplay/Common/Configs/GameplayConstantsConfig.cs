using UnityEngine;

namespace Code.Gameplay.Common.Configs
{
    [CreateAssetMenu(menuName = "Gameplay/Constants", fileName = "constantsConfig")]
    public class GameplayConstantsConfig : ScriptableObject
    {
        [Header("Common")]
        public Vector3 _farAway;
        [Header("Doors")]
        public float _doorOffset;
        public float _doorFrameVerticalOffset;
        public float _stairwellSectionVerticalOffset;
    }
}