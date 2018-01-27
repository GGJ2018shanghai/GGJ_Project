using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just For Test
public class Player : MonoBehaviour
{
    // Use this for initialization
    private GameObject player;
    void Start ()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public static float SPEED = 10;
    void Update ()
    {
        return;
        float xMove = 0, yMove = 0;
        if(Input.GetKey(KeyCode.W))
        {
            yMove += SPEED;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xMove -= SPEED;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yMove -= SPEED;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xMove += SPEED;
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 mousePosition2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mousePosition2D = Camera.main.ScreenToWorldPoint(mousePosition2D);
            Debug.Log("Here mousePosition2D is : " + mousePosition2D);
            Vector2 normalied = ((mousePosition2D - playerPosition2D)).normalized * SPEED;
            player.transform.Translate(normalied * Time.deltaTime);
        }
    }
}
