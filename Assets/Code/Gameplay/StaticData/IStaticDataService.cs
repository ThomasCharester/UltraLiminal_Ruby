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

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ItemConfig GetItemConfig(ItemID itemID);
        NPCConfig GetNPCConfig(NPCID npcID);
        CameraConfig GetCameraConfig(CameraID cameraID);
        InventoryConfig GetInventoryConfig(InventoryID inventoryID);
        LocationSegmentConfig GetLocationSegmentConfig(LocationSegmentID locationSegmentID);
        DoorConfig GetDoorConfig(DoorID doorID);
        void LoadItems();
        void LoadNPCs();
        void LoadCameras();
        void LoadInventories();
        void LoadLocationSegments();
        void LoadDoors();
        void LoadPlayer();
        void LoadGameplayConstants();
        void LoadUnityComponents();
        void LoadLocationSegmentCountInPoolConfig();
        
        public LocationSegmentsCountInPoolConfig LocationSegmentsCountInPoolConfig { get; }
        public PlayerConfig PlayerConfig { get; }
        public GameplayConstantsConfig GameplayConstantsConfig { get; }
        public UnityComponentsConfig UnityComponentsConfig { get; }
    }
}