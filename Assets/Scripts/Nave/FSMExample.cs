using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }

    public StateMachine<ExampleEnum> stateMachine;

    private void Start()
    {
        // 1. Instancia a máquina de estados passando o estado inicial padrão
        stateMachine = new StateMachine<ExampleEnum>(ExampleEnum.STATE_ONE);

        // 2. REGISTRA OS ESTADOS PRIMEIRO (Isso popula o dicionário!)
        stateMachine.RegisterStates(ExampleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_TWO, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_THREE, new StateBase());

        // 3. SÓ AGORA chama o Init() para dar o SwitchState inicial com segurança!
        stateMachine.Init();
    }

}