using UnityEngine;

namespace Code.Gameplay.Common.Configs
{
    [CreateAssetMenu(menuName = "Gameplay/Constants", fileName = "constantsConfig")]
    public class GameplayConstantsConfig : ScriptableObject
    {
        public Vector3 _farAway;
        public float _doorOffset;
    }
}