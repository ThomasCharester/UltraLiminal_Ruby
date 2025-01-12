using System;
using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Player.Animator
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int OnDeath = UnityEngine.Animator.StringToHash("onDeath");
        private static readonly int OnDamage = UnityEngine.Animator.StringToHash("onDamage");
        private static readonly int VectorForward = UnityEngine.Animator.StringToHash("VectorForward");
        private static readonly int IsRunning = UnityEngine.Animator.StringToHash("isRunning");
        private static readonly int IsAttacking = UnityEngine.Animator.StringToHash("isAttacking");
        private static readonly int InBattle = UnityEngine.Animator.StringToHash("inBattle");

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
        
        public virtual void SetMovement(float value) =>
            _animator.SetFloat(VectorForward, value - 0.01f); // Naeb animatora
        
        public virtual void PlayRun() =>
            _animator.SetBool(IsRunning, true);
        
        public virtual void StopRun() =>
            _animator.SetBool(IsRunning, false);
        
        public virtual void PlayBattleMode() =>
            _animator.SetBool(InBattle, true);
        
        public virtual void StopBattleMode() =>
            _animator.SetBool(InBattle, false);
        
        public virtual void PlayAttack() =>
            _animator.SetBool(IsAttacking, true);
        
        public virtual void StopAttack() =>
            _animator.SetBool(IsAttacking, false);
        
        public virtual void PlayDamageTaken() =>
            _animator.SetBool(OnDamage, true);
        
        public virtual void StopDamageTaken() =>
            _animator.SetBool(OnDamage, false);
        
        public virtual void PlayDeath() =>
            _animator.SetBool(OnDeath, true);
        
        public virtual void UseAegisOfImmortal() =>
            _animator.SetBool(OnDeath, false);
        
        
    }
}