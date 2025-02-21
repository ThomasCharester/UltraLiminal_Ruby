using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Player.Animator
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int OnDeath = UnityEngine.Animator.StringToHash("onDeath");

        //private static readonly int OnDamage = UnityEngine.Animator.StringToHash("onDamage");
        private static readonly int VectorForward = UnityEngine.Animator.StringToHash("VectorForward");
        private static readonly int VectorRight = UnityEngine.Animator.StringToHash("VectorRight");
        private static readonly int RotationShit = UnityEngine.Animator.StringToHash("RotationShit");
        private static readonly int IsRunning = UnityEngine.Animator.StringToHash("isRunning");
        private static readonly int IsAttacking = UnityEngine.Animator.StringToHash("isAttacking");
        private static readonly int IsJumping = UnityEngine.Animator.StringToHash("isJumping");

        [SerializeField] private UnityEngine.Animator _animator;

        private ITimeService _time;
        private float _playerTimeModifier = 0f;

        [Inject]
        public void Construct(ITimeService time)
        {
            _time = time;
        }

        private void Update()
        {
            _animator.speed = _playerTimeModifier > 0f ? _playerTimeModifier : _time.GlobalTimeModifier;
        }

        public void SetForwardMovement(float value) =>
            _animator.SetFloat(VectorForward, value);

        public void IncreaseForwardVector(float delta)
        {
            float vectorForward = _animator.GetFloat(VectorForward);
            
            if (vectorForward > 1f) return;
            
            _animator.SetFloat(VectorForward, vectorForward + delta);
        }

        public void DecreaseForwardVector(float delta)
        {
            float vectorForward = _animator.GetFloat(VectorForward);
            
            if (vectorForward < -1f) return;
            
            _animator.SetFloat(VectorForward, vectorForward - delta);
        }

        public void BringForwardVectorToZero(float delta)
        {
            float vectorForward = _animator.GetFloat(VectorForward);
            
            if (vectorForward < -0.05f)
                _animator.SetFloat(VectorForward, vectorForward + delta);
            else if (vectorForward > 0.05f) 
                _animator.SetFloat(VectorForward, vectorForward - delta);
            else 
                _animator.SetFloat(VectorForward, 0);
        }

        public void SetSideMovement(float value) =>
            _animator.SetFloat(VectorRight, value);

        public void IncreaseRightVector(float delta)
        {
            float vectorRight = _animator.GetFloat(VectorRight);
            
            if (vectorRight > 1f) return;
            
            _animator.SetFloat(VectorRight, vectorRight + delta);
        }

        public void DecreaseRightVector(float delta)
        {
            float vectorRight = _animator.GetFloat(VectorRight);
            
            if (vectorRight < -1f) return;
            
            _animator.SetFloat(VectorRight, vectorRight - delta);
        }
        public void BringRightVectorToZero(float delta)
        {
            float vectorRight = _animator.GetFloat(VectorRight);
            
            if (vectorRight < -0.05f)
                _animator.SetFloat(VectorRight, vectorRight + delta);
            else if (vectorRight > 0.05f) 
                _animator.SetFloat(VectorRight, vectorRight - delta);
            else 
                _animator.SetFloat(VectorRight, 0);
        }
        public void SetRotation(float value) =>
            _animator.SetFloat(RotationShit, value);

        public void PlayRun() =>
            _animator.SetBool(IsRunning, true);

        public void StopRun() =>
            _animator.SetBool(IsRunning, false);

        public void PlayJumpMode() =>
            _animator.SetBool(IsJumping, true);

        public void StopJumpMode() =>
            _animator.SetBool(IsJumping, false);

        public void PlayAttack() =>
            _animator.SetBool(IsAttacking, true);

        public void StopAttack() =>
            _animator.SetBool(IsAttacking, false);

        public void PlayDeath() =>
            _animator.SetBool(OnDeath, true);

        public void UseAegisOfImmortal() =>
            _animator.SetBool(OnDeath, false);
    }
}