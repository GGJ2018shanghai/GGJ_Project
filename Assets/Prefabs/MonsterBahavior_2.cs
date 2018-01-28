using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 闲晃
public class MonsterBahavior_2 : MonoBehaviour {

    public float 闲晃速度;
    public float 两次旋转间隔;
    public float 旋转时间;
    public Vector2 随机旋转角度的上下界;

    float intervalStartTime;
    // Use this for initialization
    void Start () {
        float randomAng = Random.Range(随机旋转角度的上下界.x, 随机旋转角度的上下界.y);
        transform.DOLocalRotate(new Vector3(0, 0, randomAng), 旋转时间);
        intervalStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - intervalStartTime > 两次旋转间隔)
        {
            float randomAng = Random.Range(随机旋转角度的上下界.x, 随机旋转角度的上下界.y);
            transform.DOLocalRotate(new Vector3(0, 0, randomAng), 旋转时间);
            intervalStartTime = Time.time;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.Translate(Vector3.one * 闲晃速度 * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != GameObject.Find("Player"))
        {
            Debug.Log("Emmmmmmmmm");
            transform.DOLocalRotate(new Vector3(0, 0, 180), 旋转时间);
            intervalStartTime = Time.time;
        }
    }
}
