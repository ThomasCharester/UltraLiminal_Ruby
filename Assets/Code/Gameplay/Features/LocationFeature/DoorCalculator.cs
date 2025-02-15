using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature
{
    public class DoorCalculator : MonoBehaviour
    {
        [SerializeField] List<Transform> doorOrigins;

        public List<Transform> GetDoorOrigins => doorOrigins;

        public Transform GetRandomDoorOrigin => doorOrigins[Random.Range(0, doorOrigins.Count)];
        
        
    }
}