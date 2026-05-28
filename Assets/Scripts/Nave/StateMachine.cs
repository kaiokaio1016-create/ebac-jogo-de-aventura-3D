using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        private T _initialState; // <-- Deixe APENAS ESTA declaração aqui no topo

        public StateBase CurrentState => _currentState;

        // Construtor: Apenas inicializa o dicionário e guarda o estado inicial
        public StateMachine(T state)
        {
            dictionaryState = new Dictionary<T, StateBase>();
            _initialState = state;
        }

        // Init: Dá o pontapé inicial após os RegisterStates terem rodado nos outros scripts
        public void Init()
        {
            SwitchState(_initialState);
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }

        public void SwitchState(T state)
        {
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = dictionaryState[state];

            _currentState.OnStateEnter();
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
    }
}