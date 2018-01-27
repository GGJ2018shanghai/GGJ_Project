using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public IState currentState{ get; private set;}
    public bool isCanAttack;
    public bool isCanMove;
    public bool isDie;

    public float _CoolCD;
    public float _ChaseCD;

    float CoolCD;
    float ChaseCD;

    public AIController ac;

    void Start () {
        CoolCD = 2.0f;
        ChaseCD = 1.0f;
    
    }

    public void ChangeState(IState newState) {
        currentState = newState;
        MessageDispatcher.SendMessage(this, "gcore", "state_" + newState, 0);
    }

    void Update () {
        if (currentState != null && ac != null) {
            currentState.Execute (ac);
        }
        if (isCanMove) {
            ChaseCD -= Time.deltaTime;
            if (ChaseCD <= 0) {
                isCanMove = false;
                CoolCD = _CoolCD;
            }
        } else {
            CoolCD -= Time.deltaTime;
            if (CoolCD <= 0) {
                isCanMove = true;
                ChaseCD = _ChaseCD;
            }
        }
    }
}




