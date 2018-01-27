using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState {

    public void Execute (AIController ac) {
        if (ac.fsm.isCanAttack) {
            ac.Attack();
        } else {
            ac.fsm.ChangeState(new MoveState());
        }
    }
}

