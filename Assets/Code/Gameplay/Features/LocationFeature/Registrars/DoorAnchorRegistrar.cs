using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Registrars
{
    public class DoorAnchorRegistrar: EntityComponentRegistrar
    {
        public Transform Anchor;

        public override void RegisterComponents()
        {
            // if (Entity.hasHingeJointAnchor)
            // {
            //     Vector3 anchor = Entity.HingeJointAnchor;
            //     
            //     // Как бы заменить на перемножение синусов и косинусов
            //     if (Entity.RotationSpawnPoint.y is > -45f and < 45f)
            //     {
            //         anchor.x += Anchor.localPosition.x;
            //     }
            //     else if (Entity.RotationSpawnPoint.y is >= 45f and < 135f)
            //     {
            //         anchor.z -= Anchor.localPosition.x;
            //     }
            //     else if (Entity.RotationSpawnPoint.y is >= 135f and < 225f)
            //     {
            //         anchor.x -= Anchor.localPosition.x;
            //     }
            //     else if (Entity.RotationSpawnPoint.y is >= 225f and < 305f)
            //     {
            //         anchor.z += Anchor.localPosition.x;
            //     }
            //     anchor.y += Anchor.localPosition.y;
            //
            //     Entity.ReplaceHingeJointAnchor(anchor);
            // }
            // else Entity.AddHingeJointAnchor(Anchor.position);
                
        }

        public override void UnregisterComponents()
        {
            // if (Entity.hasHingeJointAnchor)
            //     Entity.RemoveHingeJointAnchor();
        }
    }
}