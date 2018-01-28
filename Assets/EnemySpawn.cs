using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Init();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public Object[] monsterPrefabs;
    private List<Vector3> spawnPosList;
    void Awake()
    {
        spawnPosList = transform.GetComponentsInChildren<Transform>().Select(t => t.position).ToList();
        if (spawnPosList == null || spawnPosList.Count == 0)
        {
            Debug.LogError("Can not happen");
        }
    }

    Vector3 FetchPoint()
    {
        int randonIndex = Random.Range(0, spawnPosList.Count);
        var tar = spawnPosList[randonIndex];
        return tar;
    }
    void 生成怪物(int 怪物ID, int 生成数量 = 1)
    {
        for (int i = 0; i < 生成数量; i++)
        {
            var obj = Instantiate(monsterPrefabs[怪物ID], FetchPoint(), Quaternion.identity) as GameObject;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
        }
    }
    void 生成怪物(int 怪物ID, Vector3 手动指定生成位置, int 生成数量 = 1)
    {
        for (int i = 0; i < 生成数量; i++)
        {
            var obj = Instantiate(monsterPrefabs[怪物ID], 手动指定生成位置, Quaternion.identity) as GameObject;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
        }
    }
    void 设置定时生成怪物(float 间隔时间, int 怪物ID, int 生成数量 = 1)
    {
        StartCoroutine(spwan(间隔时间, 怪物ID, 生成数量));
    }
    IEnumerator spwan(float time, int id, int count)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            生成怪物(id, count);
        }
    }
    public float width;
    public float height;
    void Init()
    {
        var min = GameObject.Find("min").transform.position;
        var max = GameObject.Find("max").transform.position;
        width = max.x - min.x;
        height = max.y - min.y;

        //////////////////////////////
        生成怪物(3);
        return;
        生成怪物(0, new Vector3(9,8,0) + min);
        设置定时生成怪物(200, 0);
   //     设置定时生成怪物(2, 1);
   //     设置定时生成怪物(2, 3);
   //     设置定时生成怪物(2, 4);
  //      设置定时生成怪物(2, 5);
    }
}
