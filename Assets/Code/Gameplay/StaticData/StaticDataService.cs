using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Configs;
using Code.Gameplay.Features.Camera;
using Code.Gameplay.Features.Camera.Configs;
using Code.Gameplay.Features.Items;
using Code.Gameplay.Features.Items.Configs;
using Code.Gameplay.Features.LocationFeature;
using Code.Gameplay.Features.LocationFeature.Configs;
using Code.Gameplay.Features.NPC;
using Code.Gameplay.Features.NPC.Configs;
using Code.Gameplay.Features.Player.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<ItemID, ItemConfig> _itemsbyID;
        private Dictionary<NPCID, NPCConfig> _npcsbyID;
        private Dictionary<CameraID, CameraConfig> _camerasByID;
        private Dictionary<InventoryID, InventoryConfig> _inventoriesByID;
        private Dictionary<LocationSegmentID, LocationSegmentConfig> _locationSegmentsByID;
        private Dictionary<DoorID, DoorConfig> _doorsByID;

        private PlayerConfig _playerConfig;
        private GameplayConstantsConfig _gameplayConstantsConfig;
        private UnityComponentsConfig _unityComponentsConfig;
        private LocationSegmentsCountInPoolConfig _locationSegmentsCountInPoolConfig;

        public PlayerConfig PlayerConfig => _playerConfig;
        public GameplayConstantsConfig GameplayConstantsConfig => _gameplayConstantsConfig;
        public UnityComponentsConfig UnityComponentsConfig => _unityComponentsConfig;

        public LocationSegmentsCountInPoolConfig LocationSegmentsCountInPoolConfig =>
            _locationSegmentsCountInPoolConfig;

        public void LoadAll()
        {
            LoadPlayer();
            LoadItems();
            LoadNPCs();
            LoadCameras();
            LoadInventories();
            LoadLocationSegments();
            LoadDoors();
            LoadGameplayConstants();
            LoadUnityComponents();
            LoadLocationSegmentCountInPoolConfig();
        }

        public ItemConfig GetItemConfig(ItemID itemID)
        {
            return _itemsbyID.TryGetValue(itemID, out var value) ? value : null;
        }

        public NPCConfig GetNPCConfig(NPCID npcID)
        {
            return _npcsbyID.TryGetValue(npcID, out var value) ? value : null;
        }

        public CameraConfig GetCameraConfig(CameraID cameraID)
        {
            return _camerasByID.TryGetValue(cameraID, out var value) ? value : null;
        }

        public InventoryConfig GetInventoryConfig(InventoryID inventoryID)
        {
            return _inventoriesByID.TryGetValue(inventoryID, out var value) ? value : null;
        }

        public LocationSegmentConfig GetLocationSegmentConfig(LocationSegmentID locationSegmentID)
        {
            return _locationSegmentsByID.TryGetValue(locationSegmentID, out var value) ? value : null;
        }

        public DoorConfig GetDoorConfig(DoorID doorID)
        {
            return _doorsByID.TryGetValue(doorID, out var value) ? value : null;
        }

        public void LoadItems()
        {
            _itemsbyID = Resources
                .LoadAll<ItemConfig>("Configs/Items")
                .ToDictionary(x => x.itemID, x => x);
        }

        public void LoadNPCs()
        {
            _npcsbyID = Resources
                .LoadAll<NPCConfig>("Configs/NPCs")
                .ToDictionary(x => x.npcId, x => x);
        }

        public void LoadCameras()
        {
            _camerasByID = Resources
                .LoadAll<CameraConfig>("Configs/Cameras")
                .ToDictionary(x => x.cameraID, x => x);
        }

        public void LoadInventories()
        {
            _inventoriesByID = Resources
                .LoadAll<InventoryConfig>("Configs/Inventories")
                .ToDictionary(x => x.inventoryID, x => x);
        }

        public void LoadLocationSegments()
        {
            _locationSegmentsByID = Resources
                .LoadAll<LocationSegmentConfig>("Configs/LocationSegments")
                .ToDictionary(x => x.segmentID, x => x);
        }

        public void LoadDoors()
        {
            _doorsByID = Resources
                .LoadAll<DoorConfig>("Configs/Doors")
                .ToDictionary(x => x.doorID, x => x);
        }

        public void LoadPlayer()
        {
            _playerConfig = Resources.Load<PlayerConfig>("Configs/Player/Player");
        }

        public void LoadLocationSegmentCountInPoolConfig()
        {
            _locationSegmentsCountInPoolConfig =
                Resources.Load<LocationSegmentsCountInPoolConfig>(
                    "Configs/LocationSegments/LocationSegmentCountInPoolConfig");
        }

        public void LoadUnityComponents()
        {
            _unityComponentsConfig = Resources.Load<UnityComponentsConfig>("Configs/Gameplay/UnityComponents");
        }

        public void LoadGameplayConstants()
        {
            _gameplayConstantsConfig = Resources.Load<GameplayConstantsConfig>("Configs/Gameplay/Constants");
        }
    }
}