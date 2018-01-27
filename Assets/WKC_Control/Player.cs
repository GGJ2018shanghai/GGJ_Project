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
    void Update ()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        //GameLogicManager._OnCollisionEnter2D = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    { 
        Debug.Log("OnCollisionExit2D");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("OnCollisionStay2D");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<MonsterDesc>().GetKilled();
        GameLogicManager.Instance.Encounter(collision.GetComponent<MonsterDesc>());
        Destroy(collision.gameObject);
    }
}
