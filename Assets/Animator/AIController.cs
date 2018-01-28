using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AIController : MonoBehaviour {

    public FSM fsm;
    GameObject player;
    CountPath counter;
    Transform trans;

    float MoveSpeed;
    int AIType;

    public enum EAIType
    {
        类型1,
        类型2,
        类型3
    }
    public EAIType eaitype;
    public float 类型1_寻路移动速度 = 3;
    public float 类型3_抖动持续时间 = .5f;
    public float 类型3_抖动伸展度 = 1;
    public int 类型3_抖动震动强度 = 5;

    void Start () {
        fsm = GetComponent<FSM> ();
        fsm.ac = this;
        player = GameObject.Find("Player");
        counter = GetComponent<CountPath>();

        switch (eaitype)
        {
            case EAIType.类型1:
                AIType = 1;
                break;
            case EAIType.类型2:
                AIType = 2;
                break;
            case EAIType.类型3:
                AIType = 3;
                break;
            default:
                float tmpRandom = Random.value;
                if (tmpRandom <= 0.3f)
                {
                    // chase and stop AI
                    AIType = 1;
                }
                else if (tmpRandom <= 0.6f)
                {
                    // random rotation
                    AIType = 2;
                }
                else
                {
                    // up and down
                    AIType = 3;
                }
                break;
        }

        //初始化一个默认状态机
        //fsm.ChangeState(new MoveState());
    }

    void Update() {

        counter.movespeed = 类型1_寻路移动速度;

        if (AIType == 1) {
            LogicOne();
        } else if (AIType == 2) {
            LogicTwo();
        } else if (AIType == 3) {
            LogicThree();
        }

    }

    float dashTime;
    float lastAdjustTime;
    Vector2 MoveDirection;
    Vector2 AdjustDirection;
    void LogicTwo() {
        
        if (Random.value >= 0.5) {
            float rotateAng = Random.Range(0, 360);
            transform.DOLocalRotate(new Vector3(0, 0, rotateAng), rotateAng / 360 * 2);
        }

        fsm.UpdateJumpCD();
        //Debug.LogError(transform.position);
        if (fsm.JumpCD <= 0) {
            fsm.JumpCD = 3.0f;
            dashTime = 1f;
            float XDiretion = Random.value - 0.5f;
            float YDiretion = Random.value - 0.5f;
            MoveDirection = new Vector2(XDiretion, YDiretion).normalized;
        }
        
        if (dashTime > 0f)
        {
            if(lastAdjustTime - dashTime > 0.2f)
            {
                float XDiretion = Random.value - 0.5f;
                float YDiretion = Random.value - 0.5f;
                AdjustDirection = new Vector2(XDiretion, YDiretion).normalized;
            }
            this.transform.Translate(10f * dashTime * dashTime * Time.deltaTime * (MoveDirection + AdjustDirection));
            dashTime -= Time.deltaTime;
        }
    }

    void LogicThree() {
        // adjust parameters
        transform.DOShakePosition(类型3_抖动持续时间, 类型3_抖动伸展度, 类型3_抖动震动强度, 90f, false, true);
    }

    void LogicOne() {

        if (fsm.isCanMove) {
            // can move
            fsm.UpdateChaseCD();
                counter.movespeed = 类型1_寻路移动速度;
            if (fsm.IsChaseCDLessThanZero())
            {
                fsm.isCanMove = false;

                fsm.InitCoolAndChaseCD();
                fsm.ChangeState(new IdleState());
            }
        } else {
            // can't move
            fsm._CoolCD = 1; 
            fsm.UpdateCoolCD();
            Debug.Log("eMMM");
            if (fsm.IsCoolCDLessThanZero()) {
            Debug.Log("eMMMwwww");
                counter.movespeed = 0;
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
