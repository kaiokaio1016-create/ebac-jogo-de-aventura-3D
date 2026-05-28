using UnityEngine;
using Ebac.StateMachine;

namespace Ebac.StateMachine
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterControllerFSM : MonoBehaviour
    {
        public enum CharacterStates
        {
            IDLE,
            WALK,
            JUMP
        }

        [Header("Configurações de Movimento")]
        public float moveSpeed = 8f;
        public float jumpForce = 12f;

        [Header("Verificação de Chão")]
        public Transform groundCheckTransform;
        public float groundCheckRadius = 0.3f;
        public LayerMask groundLayer;

        [HideInInspector]
        public StateMachine<CharacterStates> stateMachine;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            // Instancia a Máquina de Estados apontando o estado padrão inicial
            stateMachine = new StateMachine<CharacterStates>(CharacterStates.IDLE);

            // Regista os estados no dicionário interno
            stateMachine.RegisterStates(CharacterStates.IDLE, new CharacterStateIdle(this));
            stateMachine.RegisterStates(CharacterStates.WALK, new CharacterStateWalk(this));
            stateMachine.RegisterStates(CharacterStates.JUMP, new CharacterStateJump(this));

            // Inicializa a máquina por último com total segurança
            stateMachine.Init();
        }

        private void Update()
        {
            // O uso do '?.' impede erros de NullReferenceException no Inspector
            stateMachine?.Update();
        }

        public void MoveForward()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputZ = Input.GetAxisRaw("Vertical");

            // Calcula a direção baseado na orientação da câmara/mundo para o boneco
            Vector3 moveDirection = (transform.forward * inputZ) + (transform.right * inputX);
            moveDirection.Normalize();

            // Aplica a velocidade mantendo o cálculo da gravidade do Rigidbody intacto
            _rb.velocity = new Vector3(moveDirection.x * moveSpeed, _rb.velocity.y, moveDirection.z * moveSpeed);

            // Roda o modelo suavemente para encarar a direção para onde caminha
            if (moveDirection != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 10f);
            }
        }

        public void ApplyJumpForce()
        {
            // Aplica o impulso vertical puro para o salto
            _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
        }

        public void ResetVelocityX()
        {
            // Retira a inércia horizontal para o boneco travar imediatamente ao parar
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }

        public float GetVelocityY()
        {
            return _rb.velocity.y;
        }

        public bool IsGrounded()
        {
            if (groundCheckTransform != null)
            {
                return Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundLayer);
            }
            return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        }

        private void OnDrawGizmos()
        {
            // CORRIGIDO: A linha do desenho está agora bem guardada dentro do IF protetor!
            if (groundCheckTransform != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
            }
        }
    }
}