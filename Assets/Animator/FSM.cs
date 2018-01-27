using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public IState currentState{ get; private set;}
    public bool isCanAttack;
    public bool isCanMove;
    public bool isDie;

    public float JumpSpeed;
    public float JumpCD;

    public float _CoolCD;
    public float _ChaseCD;

    float CoolCD;
    float ChaseCD;

    public AIController ac;

    void Start () {
        CoolCD = 2.0f;
        ChaseCD = 1.0f;

        JumpCD = 3.0f;
        JumpSpeed = 1.0f;
    }

    public void UpdateJumpCD() {
        JumpCD -= Time.deltaTime;
    }

    public void ChangeState(IState newState) {
        currentState = newState;
        MessageDispatcher.SendMessage(this, "gcore", "state_" + newState, 0);
    }

    public void InitCoolAndChaseCD() {
        CoolCD = _CoolCD;
        ChaseCD = _ChaseCD;
    }

    public bool IsCoolCDLessThanZero() {
        if (CoolCD <= 0) return true;
        return false;
    }

    public void UpdateChaseCD() {
        ChaseCD -= Time.deltaTime;
    }

    public void UpdateCoolCD() {
        CoolCD -= Time.deltaTime;
    }

    public bool IsChaseCDLessThanZero() {
        if (ChaseCD <= 0) return true;
        return false;
    }

    void Update () {
        if (currentState != null && ac != null) {
            currentState.Execute (ac);
        }
    }
}




