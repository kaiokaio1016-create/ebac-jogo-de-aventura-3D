using UnityEngine;
using Ebac.StateMachine;

namespace Ebac.StateMachine
{
    // ==========================================
    // --- ESTADO PARADO (IDLE) ---
    // ==========================================
    public class CharacterStateIdle : StateBase
    {
        private CharacterControllerFSM _character;

        public CharacterStateIdle(CharacterControllerFSM character)
        {
            _character = character;
        }

        public override void OnStateEnter(object o = null)
        {
            Debug.Log("Entrou no Estado: PARADO");
            _character.ResetVelocityX();
        }

        public override void OnStateStay()
        {
            // Transiciona para o Pulo se estiver no chão e premir Espaço
            if (Input.GetKeyDown(KeyCode.Space) && _character.IsGrounded())
            {
                _character.stateMachine.SwitchState(CharacterControllerFSM.CharacterStates.JUMP);
                return;
            }

            // Se detetar qualquer comando no WASD ou Setas, vai para o Walk
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                _character.stateMachine.SwitchState(CharacterControllerFSM.CharacterStates.WALK);
            }
        }
    }

    // ==========================================
    // --- ESTADO ANDAR (WALK) ---
    // ==========================================
    public class CharacterStateWalk : StateBase
    {
        private CharacterControllerFSM _character;

        public CharacterStateWalk(CharacterControllerFSM character)
        {
            _character = character;
        }

        public override void OnStateEnter(object o = null)
        {
            Debug.Log("Entrou no Estado: ANDANDO");
        }

        public override void OnStateStay()
        {
            // Se o jogador soltar todas as teclas de movimento, volta para o IDLE
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                _character.stateMachine.SwitchState(CharacterControllerFSM.CharacterStates.IDLE);
                return;
            }

            // Se premir Espaço enquanto corre, salta corretamente
            if (Input.GetKeyDown(KeyCode.Space) && _character.IsGrounded())
            {
                _character.stateMachine.SwitchState(CharacterControllerFSM.CharacterStates.JUMP);
                return;
            }

            // Aplica o movimento contínuo
            _character.MoveForward();
        }
    }

    // ==========================================
    // --- ESTADO PULAR (JUMP) ---
    // ==========================================
    public class CharacterStateJump : StateBase
    {
        private CharacterControllerFSM _character;

        public CharacterStateJump(CharacterControllerFSM character)
        {
            _character = character;
        }

        public override void OnStateEnter(object o = null)
        {
            Debug.Log("Entrou no Estado: PULANDO");
            _character.ApplyJumpForce();
        }

        public override void OnStateStay()
        {
            // Só volta para o IDLE quando estiver a cair (Y <= 0.01f) E tocar no chão de novo
            if (_character.GetVelocityY() <= 0.01f && _character.IsGrounded())
            {
                _character.stateMachine.SwitchState(CharacterControllerFSM.CharacterStates.IDLE);
                return;
            }

            // Permite ajustar a direção e mover-se ligeiramente enquanto está no ar!
            _character.MoveForward();
        }
    }
}