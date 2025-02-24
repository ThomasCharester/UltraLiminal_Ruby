using System.Collections.Generic;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class TurnOnDoorSystem : ReactiveSystem<GameEntity>
    {
        private IStaticDataService _staticDataService;

        [Inject]
        public TurnOnDoorSystem(Contexts contexts, IStaticDataService staticDataService) : base(contexts.game)
        {
            _staticDataService = staticDataService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(
                GameMatcher.HingeJointAnchorPosition,
                GameMatcher.HingeJointAnchorRotation));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHingeJoint && entity.hasHingeJointAnchorPosition && entity.hasHingeJointAnchorRotation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                // entity.View.gameObject.AddComponent<HingeJoint>();
                // entity.AddHingeJoint(entity.View.gameObject.GetComponent<HingeJoint>());
                //
                // entity.View.gameObject.GetComponent<HingeJoint>().anchor =
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.anchor;
                // entity.View.gameObject.GetComponent<HingeJoint>().autoConfigureConnectedAnchor =
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.autoConfigureConnectedAnchor;
                // entity.View.gameObject.GetComponent<HingeJoint>().axis =
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.axis;
                // entity.View.gameObject.GetComponent<HingeJoint>().useSpring =
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.useSpring;
                // entity.View.gameObject.GetComponent<HingeJoint>().spring =
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.spring;
                // entity.View.gameObject.GetComponent<HingeJoint>().useLimits = 
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.useLimits;
                // entity.View.gameObject.GetComponent<HingeJoint>().limits = 
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.limits;
                // entity.View.gameObject.GetComponent<HingeJoint>().massScale = 
                //     _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.massScale;

                Vector3 anchor = _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.anchor; //Vector3.zero;
                Vector3 connectedAnchor = entity.HingeJointAnchorPosition;

                if (entity.HingeJointAnchorRotation.eulerAngles.y is > -45f and < 45f)
                {
                    connectedAnchor.x += anchor.x;
                    //connectedAnchor.z += _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.anchor.x;
                }
                else if (entity.HingeJointAnchorRotation.eulerAngles.y is >= 45f and < 135f)
                {
                    connectedAnchor.z -= anchor.x;
                }
                else if (entity.HingeJointAnchorRotation.eulerAngles.y is >= 135f and < 225f)
                {
                    connectedAnchor.x -= anchor.x;
                }
                else if (entity.HingeJointAnchorRotation.eulerAngles.y is >= 225f and < 305f)
                {
                    connectedAnchor.z += anchor.x;
                }

                connectedAnchor.y += _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.anchor.y;
                //anchor.y += _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.anchor.y;

                entity.HingeJoint.anchor = anchor;

                entity.HingeJoint.connectedAnchor = connectedAnchor;

                entity.RemoveHingeJointAnchorPosition();
                entity.RemoveHingeJointAnchorRotation();
            }
        }
    }
}