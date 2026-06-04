using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation; 

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor FlashColor;
        public ParticleSystem _particleSystem;

        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            _currentLife = startLife; // Isso faz com que o Current Life receba o valor de Start Life ao iniciar
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();

            if (startWithBornAnimation)
                BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            Debug.Log("Dano recebido: " + f); // Veja no console se este valor é maior que 0
            _currentLife -= f;

            if (FlashColor != null) FlashColor.Flash();
            if (_particleSystem != null) _particleSystem.Emit(15);

            if (_currentLife <= 0)
            {
                Debug.Log("Vida chegou a zero, matando inimigo.");
                Kill();
            }
        }

        #region ANIMATION
        private void BornAnimation()
        {
            // Utilisation de .From() pour animer de 0 vers la taille actuelle (1)
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            if (_animationBase != null)
            {
                _animationBase.PlayAnimationByTrigger(animationType);
            }
        }
        #endregion

        //debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

    }
}