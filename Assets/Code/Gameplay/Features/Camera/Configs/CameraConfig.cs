using Code.Infrastructure.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Camera.Configs
{
    [CreateAssetMenu(menuName = "Cameras", fileName = "cameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public CameraID cameraID;
        public EntityBehaviour cameraPrefab;
    }
}