using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public FSM fsm;
    public GameObject player;
    CountPath counter;
    

    void Start () {
        fsm = GetComponent<FSM> ();
        fsm.ac = this;
        player = GameObject.Find("Player");
        counter = GetComponent<CountPath>();


        //初始化一个默认状态机
        fsm.ChangeState(new MoveState());
    }

    void Update() {

        if (fsm.isCanMove) {
            fsm.UpdateChaseCD();
            if (fsm.IsChaseCDLessThanZero()) { 
                fsm.isCanMove = false;
                fsm.InitCD();
                fsm.ChangeState(new IdleState());
            }
        } else {
            fsm.UpdateCoolCD();
            if (fsm.IsCoolCDLessThanZero()) {
                fsm.isCanMove = true;
                fsm.InitCD();
                fsm.ChangeState(new MoveState());
            }
        }
    }

    public void Move() {
        //Debug.Log("move");

        //couter.FindPath(player.transform, player.transform.position);
        counter.FindPath(transform, player.transform.position);
    
    }

    public void Attack() {
        Debug.Log("attack");
    }

    public void Die() {

    }

    public void Idle() {

    }

}
