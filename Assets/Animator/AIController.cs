using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public FSM fsm;
    private NavMeshAgent agent;
    public GameObject player;

    void Start () {
        fsm = GetComponent<FSM> ();
        fsm.ac = this;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");


        //初始化一个默认状态机
        fsm.ChangeState(new MoveState());
    }

    void Update() {

    }

    public void Move() {
        Debug.Log("move");

        agent.SetDestination(player.transform.position);

    }

    public void Attack() {
        Debug.Log("attack");
    }

    public void Die() {

    }

    public void Idle() {

    }

}
