using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;

namespace Ebac.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameStates
        {
            INTRO,
            GAMEPLAY,
            PAUSE,
            WIN,
            LOSE
        }

        public StateMachine<GameStates> stateMachine;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            // 1. Instancia a máquina apontando que o primeiro estado deve ser INTRO
            stateMachine = new StateMachine<GameStates>(GameStates.INTRO);

            // 2. Preenche o dicionário primeiro
            stateMachine.RegisterStates(GameStates.INTRO, new StateBase());
            stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
            stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
            stateMachine.RegisterStates(GameStates.WIN, new StateBase());
            stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

            // 3. AGORA SIM, com o dicionário cheio, inicializa com segurança!
            stateMachine.Init();
        }
    }
}