using System;
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
                Vector3 anchor = _staticDataService.UnityComponentsConfig._defaultDoorHingeJoint.anchor; 
                Vector3 connectedAnchor = entity.HingeJointAnchorPosition;

                // if (entity.HingeJointAnchorRotation.eulerAngles.y is > -45f and < 225f)
                // {
                //     connectedAnchor.x += anchor.x * Mathf.Clamp01(Mathf.Cos(Mathf.Deg2Rad * anchor.x));
                //     connectedAnchor.z += anchor.x * Mathf.Clamp01(Mathf.Sin(Mathf.Deg2Rad * anchor.x));
                // }
                // else
                // {
                //     connectedAnchor.x += anchor.x * Mathf.Clamp01(Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * anchor.x)));
                //     connectedAnchor.z += anchor.x * Mathf.Clamp01(Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * anchor.x)));
                // }
                if (entity.HingeJointAnchorRotation.eulerAngles.y is > -45f and < 45f)
                {
                    connectedAnchor.x += anchor.x;
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

                entity.HingeJoint.anchor = anchor;

                entity.HingeJoint.connectedAnchor = connectedAnchor;

                entity.RemoveHingeJointAnchorPosition();
                entity.RemoveHingeJointAnchorRotation();
            }
        }
    }
}