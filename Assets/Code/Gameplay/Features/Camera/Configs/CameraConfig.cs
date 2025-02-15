using Code.Infrastructure.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Camera.Configs
{
    [CreateAssetMenu(menuName = "CameraConfig", fileName = "cameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public CameraID cameraID;
        public EntityBehaviour cameraPrefab;
    }
}