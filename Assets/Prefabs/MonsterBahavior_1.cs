using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 追踪玩家
public class MonsterBahavior_1 : MonoBehaviour {

    public float 追踪的持续时间;
    public float 追踪的cd时间;
    public float 追踪速度;
    public bool 先CD;

    private CountPath countpath;
    private float cdStartTime;
    private float chaseStartTime;
    bool isCDMode;

    // Use this for initialization
    void Start () {
        countpath = GetComponent<CountPath>();
        isCDMode = 先CD;
        if (isCDMode) cdStartTime = Time.time;
        else chaseStartTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        countpath.movespeed = 追踪速度;
        if (isCDMode)
        {
            if (Time.time - cdStartTime > 追踪的cd时间)
            {
                // start chase
                chaseStartTime = Time.time;
                countpath.FindPath(transform, GameObject.Find("Player").transform.position);

                isCDMode = false;
            }
            // else pause
        }
        else
        {
            // chase mode
            if (Time.time - chaseStartTime > 追踪的持续时间)
            {
                isCDMode = true;
                // start chase
                cdStartTime = Time.time;
                countpath.StopMovement();

            }
        }
        
	}

}
