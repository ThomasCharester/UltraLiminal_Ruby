using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature
{
    public class DoorCalculator : MonoBehaviour
    {
        [SerializeField] private List<Transform> doorOrigins;
        [SerializeField] private Transform playerStart;
        public List<Transform> GetDoorOrigins 
        {
            get { return doorOrigins; }
            private set { }
        }
        public Transform GetPlayerStart => playerStart;

        public Transform GetRandomDoorOrigin => doorOrigins[Random.Range(0, doorOrigins.Count)];
        
        
    }
}