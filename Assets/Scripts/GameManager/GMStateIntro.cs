using UnityEngine; // <-- Garanta que essa linha existe no topo!
using Ebac.StateMachine; // Namespace onde está o seu StateBase

namespace GameManager
{
    // Corrija a herança aqui: mude de MonoBehaviour para StateBase
    public class GMStateIntro : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            Debug.Log("Entrou no Estado Intro do GameManager");
        }

        public override void OnStateStay()
        {
            // Lógica do estado aqui...
        }

        public override void OnStateExit()
        {
            Debug.Log("Saindo do Estado Intro");
        }
    }
}