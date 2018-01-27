using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public IState currentState{ get; private set;}
    public bool isCanAttack;
    public bool isCanMove;
    public bool isDie;

    public AIController ac;

    void Start () {
    
    }

    public void ChangeState(IState newState) {
        currentState = newState;
        MessageDispatcher.SendMessage(this, "gcore", "state_" + newState, 0);
    }

    void Update () {
        if (currentState != null && ac != null) {
            currentState.Execute (ac);
        }
    }
}




