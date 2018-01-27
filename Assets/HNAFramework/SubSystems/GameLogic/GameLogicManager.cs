using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : SystemBase<GameLogicManager, GameLogicManagerData> {

    // Use this for initialization
    GameObject player;
	new void Awake ()
    {
        base.Awake();
        Debug.Log("Data.hp = " + Data.hp);
        Debug.Log("Data.speed = " + Data.speed);
        player = GameObject.Find("Player");
    }

    // 速度Buff：外部函数直接调用函数GameLogicManager.Instance.ApplySpeedBuffer() 来执行具体的修改函数
    public void ApplySpeedBuffer(float buffValue, float bufferTime)
    {
        StartCoroutine(_ApplySpeedBuffer(buffValue, bufferTime));
    }
    IEnumerator _ApplySpeedBuffer(float buffValue, float bufferTime)
    {
        Data.speed += buffValue;
        yield return new WaitForSeconds(bufferTime);
        Data.speed -= buffValue;
    }

    // 尺寸Buff：外部函数直接调用函数GameLogicManager.Instance.ApplySpeedBuffer() 来执行具体的修改函数
    public void ApplySizeBuffer(float buffValue, float bufferTime)
    {
        StartCoroutine(_ApplySizeBuffer(buffValue, bufferTime));
    }
    IEnumerator _ApplySizeBuffer(float buffValue, float bufferTime)
    {
        Data.size += buffValue;
        yield return new WaitForSeconds(bufferTime);
        Data.size -= buffValue;
    }

    private float pressTime = 0, loseTime = 0;
    private float StartSpeed()
    {
        return pressTime < 1f ? pressTime : 1f;
    }
    private float EndSpeed()
    {
        return loseTime < 1f ? loseTime : 1f;
    }
    private void Update()
    {
        float xMove = 0, yMove = 0;
        if (Input.GetKey(KeyCode.W))
        {
            yMove += Data.speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xMove -= Data.speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yMove -= Data.speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xMove += Data.speed;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 mousePosition2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mousePosition2D = Camera.main.ScreenToWorldPoint(mousePosition2D);
            Debug.Log("Here press mousePosition2D is : " + mousePosition2D);
            Vector2 normalied = ((mousePosition2D - playerPosition2D)).normalized * Data.speed * StartSpeed();
            player.transform.Translate(normalied * Time.deltaTime);
            pressTime += Time.deltaTime;
        }
        else
        {
            if (pressTime > 0f)
            {
                loseTime = pressTime < 0.5f ? pressTime : 0.5f;
                pressTime = 0;
            }
            loseTime -= Time.deltaTime;
            if (loseTime > 0f)
            {
                Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.y);
                Vector2 mousePosition2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                mousePosition2D = Camera.main.ScreenToWorldPoint(mousePosition2D);
                Debug.Log("Here lose mousePosition2D is : " + mousePosition2D);
                Vector2 normalied = ((mousePosition2D - playerPosition2D)).normalized * Data.speed * EndSpeed();
                player.transform.Translate(normalied * Time.deltaTime);
            }
        }
    }
}
