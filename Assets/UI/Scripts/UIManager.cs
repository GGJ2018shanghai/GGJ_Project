using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject continueButton;
    public GameObject[] cards;

    public GameObject upgradePanel;
    public GameObject[] upgradeButton;
    public GameObject restartButton;

    public GameObject upgradeBlockPrefab;

    //临时使用
    int money;
    public GameObject moneyText;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        ShowPanel();

        upgradeButton[0].GetComponent<Upgrade>().upgradeName = "血量";
        upgradeButton[1].GetComponent<Upgrade>().upgradeName = "速度";
        upgradeButton[2].GetComponent<Upgrade>().upgradeName = "回血";
        upgradeButton[3].GetComponent<Upgrade>().upgradeName = "掉落";
    }

    // Update is called once per frame
    void Update ()
    {
        money = GameLogicManager.AP;
        Debug.Log("！！！ Money = " + money);
        moneyText.GetComponent<Text>().text = ""+money+" !";
	}

    public void HideUI(GameObject ui)
    {

        Transform uiTransform = ui.transform;
        uiTransform.DOScaleY(0, 0.20f).SetEase(Ease.InExpo).OnComplete( ()=> {
            ui.SetActive(false);

        });
    }



    public void ShowUI(GameObject ui)
    {
        ui.SetActive(true);

        Transform uiTransform = ui.transform;
        uiTransform.DOScaleY(0, 0.35f).From().SetEase(Ease.InExpo);
    }

    public void ShowPanel()
    {
        ShowUI(upgradePanel);
        

        //逐一出现卡片和更新数字
        StartCoroutine(ShowCards());
        

    }

    IEnumerator ShowCards()
    {
        for(int i = 0; i < 6; i++)
        {
            cards[i].transform.DORotate(new Vector3(0, 90, 0), 0.5f).From();
            cards[i].GetComponentInChildren<Text>().text = "1";
            cards[i].GetComponentInChildren<Text>().gameObject.transform.DOScale(new Vector2(2, 2), 0.9f).From();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator HideCards()
    {
        //旋转卡片
        for (int i = 0; i < 6; i++)
        {
            cards[i].transform.DORotate(new Vector3(0, 90, 0), 0.35f);
            cards[i].transform.DOScaleY(1.5f, 0.35f);
            yield return new WaitForSeconds(0.1f);
        }

    }

    public void MovePanel()
    {
        
        //移动Panel
        upgradePanel.transform.DOMove(transform.position + new Vector3(960, 720, 0), 1.7f).SetEase(Ease.InQuad);

        GameObject.Find("Transmission").transform.DOLocalMoveY(-100, 2.8f).SetEase(Ease.OutBounce);
        //GameObject.Find("DownBox").transform.DOScale(1.2f, 0.15f).From().SetEase(Ease.InCirc);
    }

    public void OnClickContinueButton(bool isOn)
    {
        StartCoroutine(HideCards());
        continueButton.SetActive(false);
        restartButton.SetActive(true);
        Invoke("MovePanel",0.01f);
        
    }
    public void OnClickRestartButton(bool isOn)
    {
        HideUI(upgradePanel);
        //重新开始游戏
        SceneManager.LoadScene("Main");
        GameLogicManager.Instance.Data.hp = 100 + upgradeButton[0].GetComponent<Upgrade>().lv * 20;
    }

    public bool CostMoney(int needMoney)
    {
        
        bool isOk=false;
        if (money >= needMoney)
        {
            isOk = true;
            //消耗代币
            money -= needMoney;
            
        }
        return isOk;
    }

    public void ShakeMoneyText()
    {
        moneyText.transform.DOScale(1.5f, 0.2f).From().SetEase(Ease.InOutCubic);
    }

   public  void UpdateLv(GameObject button)
    {
        //等级提升的图像
        int distance = button.GetComponent<Upgrade>().lv*50;
        Debug.Log(distance);
        Vector2 pos = new Vector2(100+button.transform.position.x+distance, button.transform.position.y);
        GameObject block = Instantiate(upgradeBlockPrefab, button.transform.position, Quaternion.identity);
        block.transform.SetParent(GameObject.Find("Blocks").transform);
        block.transform.position = pos;        
        block.transform.DOMove(pos-new Vector2(50,0),0.2f).From().OnComplete(() => {
            block.transform.DOScaleX(0.5f, 0.18f).From();
        });
    }
}
