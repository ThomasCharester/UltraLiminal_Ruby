using UnityEngine;

namespace Code.Gameplay.Common.Configs
{
    [CreateAssetMenu(menuName = "Gameplay/UnityComponents", fileName = "unityComponentsConfig")]
    public class UnityComponentsConfig : ScriptableObject
    {
        public HingeJoint _defaultDoorHingeJoint;
    }
}