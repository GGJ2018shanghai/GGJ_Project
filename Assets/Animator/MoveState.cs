using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState {

    public void Execute (AIController ac) {
        if (ac.fsm.isCanMove) {
            ac.Move();
        } else {
            ac.fsm.ChangeState(new IdleState());
        }
    }
}

