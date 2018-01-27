using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AIController : MonoBehaviour {

    public FSM fsm;
    public GameObject player;
    CountPath counter;
    Transform trans;

    float MoveSpeed;
    int AIType;
    

    void Start () {
        fsm = GetComponent<FSM> ();
        fsm.ac = this;
        player = GameObject.Find("Player");
        counter = GetComponent<CountPath>();

        float tmpRandom = Random.value;
        if (tmpRandom <= 0.3f) {
            // chase and stop AI
            AIType = 1;
        } else if (tmpRandom <= 0.6f) {
            // random rotation
            AIType = 2;
        } else {
            // up and down
            AIType = 3;
        }

        //初始化一个默认状态机
        fsm.ChangeState(new MoveState());
    }

    void Update() {

        if (AIType == 1) {
            LogicOne();
        } else if (AIType == 2) {
            //LogicTwo();
        } else if (AIType == 3) {
            LogicThree();
        }

    }


    void LogicTwo() {


        if (Random.value >= 0.5) {
            float rotateAng = Random.Range(0, 360);
            transform.DOLocalRotate(new Vector3(0, 0, rotateAng), rotateAng / 360 * 2);
        }

        fsm.UpdateJumpCD();
        Debug.LogError(transform.position);
        if (fsm.JumpCD <= 0) {
            Debug.LogError("Emmmm");
            GetComponent<DG.Tweening.DOTweenAnimation>().endValueV3 = new Vector3(fsm.JumpSpeed*50, 0f);
            GetComponent<DG.Tweening.DOTweenAnimation>().isRelative = true;
            GetComponent<DG.Tweening.DOTweenAnimation>().DOPlayForward();
            GetComponent<DG.Tweening.DOTweenAnimation>().DOKill();
            fsm.JumpCD = 3.0f;

        }


    }

    void LogicThree() {
        // adjust parameters
        transform.DOShakePosition(0.5f, 1f, 5, 90f, false, true);
    }

    void LogicOne() {

        if (fsm.isCanMove) {
            // can move
            fsm.UpdateChaseCD();
            if (fsm.IsChaseCDLessThanZero()) { 
                fsm.isCanMove = false;

                fsm.InitCoolAndChaseCD();
                fsm.ChangeState(new IdleState());
            }
        } else {
            // can't move
            fsm.UpdateCoolCD();
            if (fsm.IsCoolCDLessThanZero()) {
                fsm.isCanMove = true;
                fsm.InitCoolAndChaseCD();
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
