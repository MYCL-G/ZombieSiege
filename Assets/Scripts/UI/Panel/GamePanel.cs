using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePanel : BasePanel
{
    public TextMeshProUGUI textHp;
    public TextMeshProUGUI textWave;
    public TextMeshProUGUI textGold;
    public Image imgHp;
    public Button btnBack;
    public Transform botTrans;
    public List<towerBtn> towerBtnList = new List<towerBtn>();
    bool checkInput = false;
    TowerPoint nowTowerPoint;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            mUI.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        botTrans.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UpdateHp(int hp, int maxHp)
    {
        textHp.text = hp + "/" + maxHp;
        (imgHp.transform as RectTransform).sizeDelta = new Vector2((float)hp / maxHp * 500, 60);
    }
    public void UpdateWave(int nowWave, int maxWave)
    {
        textWave.text = nowWave + "/" + maxWave;
    }
    public void UpdateGold(int gold)
    {
        textGold.text = gold.ToString();
    }
    public void UpdateTower(TowerPoint point)
    {
        nowTowerPoint = point;
        if (nowTowerPoint == null)
        {
            checkInput=false;
            botTrans.gameObject.SetActive(false);
        }
        else
        {
            checkInput=true;
            botTrans.gameObject.SetActive(true);
            if (nowTowerPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < towerBtnList.Count; i++)
                {
                    towerBtnList[i].gameObject.SetActive(true);
                    towerBtnList[i].InitInfo(nowTowerPoint.chooseIdList[i], "数字键" + (i + 1));
                }
            }
            else
            {
                for (int i = 0; i < towerBtnList.Count; i++)
                {
                    towerBtnList[i].gameObject.SetActive(false);
                    towerBtnList[i].InitInfo(nowTowerPoint.chooseIdList[i], "数字键" + (i + 1));
                }
                towerBtnList[1].gameObject.SetActive(true);
                towerBtnList[1].InitInfo(nowTowerPoint.nowTowerInfo.next, "空格键");
            }
        }
    }
    private void Update()
    {
        if (checkInput)
        {
            if (nowTowerPoint.nowTowerInfo == null)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    nowTowerPoint.CreateTower(nowTowerPoint.chooseIdList[0]);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    nowTowerPoint.CreateTower(nowTowerPoint.chooseIdList[1]);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    nowTowerPoint.CreateTower(nowTowerPoint.chooseIdList[2]);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    nowTowerPoint.CreateTower(nowTowerPoint.nowTowerInfo.next);
                }
            }
        }

    }
}
