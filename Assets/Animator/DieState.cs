using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState {

    public void Execute (AIController ac) {
        MessageDispatcher.SendMessage(this, "gcore", "enemydie", 0);
    }
}

