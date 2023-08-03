using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class towerBtn : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI textTip;
    public TextMeshProUGUI textPirce;
    public void InitInfo(int id, string inputStr)
    {
        TowerInfo towerInfo = mGameData.Instance.towerInfoList[id - 1];
        image.sprite = Resources.Load<Sprite>(towerInfo.imgRes);
        textPirce.text = "$" + towerInfo.money;
        textTip.text = inputStr;
        if (towerInfo.money > mGameData.Instance.playerData.goldMoney)
        {
            textPirce.text="金钱不足";
        }
    }
}
