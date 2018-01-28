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

    public void Encounter(MonsterDesc desc)
    {
        
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
    private Vector2 dashDirection;
    private float dashTime = 0;
    private bool dashGO = false;
    private float DashSpeed()
    {
        return dashTime < 1f ? dashTime : 1f;
    }

    public static bool _OnCollisionEnter2D = false;
    private void Update()
    {
        //player.transform.localScale = new Vector2(Data.size, Data.size);
        
        if (_OnCollisionEnter2D)
        {
            dashGO = false;
            _OnCollisionEnter2D = false;
        }

        //这里是我们Charge类型的移动方式
        if (!dashGO && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 mousePosition2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mousePosition2D = Camera.main.ScreenToWorldPoint(mousePosition2D);
            dashDirection = ((mousePosition2D - playerPosition2D)).normalized * Data.speed ;
            dashTime = Data.botDashTime;
        }
        if (!dashGO && Input.GetKey(KeyCode.Mouse0))
        {
            dashTime += Time.deltaTime;
            if (dashTime < Data.topDashTime) 
                dashDirection += dashDirection * Time.deltaTime / Data.botDashTime;
            //if (dashTime > Data.topDashTime) dashTime = Data.topDashTime;
        }

        //抬起按键之后
        if(!dashGO && Input.GetKeyUp(KeyCode.Mouse0))
        {
            dashGO = true;
            dashTime = Data.botDashTime;
        }
        if(dashGO)
        {
            if(dashTime>= 0.7*Data.botDashTime)
            {
                player.transform.Translate(dashDirection * Time.deltaTime);
            }
            else if(dashTime>= 0.4 * Data.botDashTime)
            {
                player.transform.Translate( dashDirection * Time.deltaTime * dashTime/ Data.topDashTime*0.5f);
            }
            

            dashTime -= Time.deltaTime;
            if(dashTime < 0f)
            {
                dashGO = false;
            }
        }

        //接下来是我们初始的移动方式
        return;
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
