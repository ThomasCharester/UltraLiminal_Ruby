using Code.Infrastructure.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Common
{
    public class GameplayConstants : MonoBehaviour
    {
        public Vector3 farAway = new Vector3(999f, 999f, 999f);
        public Vector3 zero = Vector3.zero;
    }
}