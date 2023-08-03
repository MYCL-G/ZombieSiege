using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPanel : BasePanel
{
    public Button btnOk;
    public TextMeshProUGUI textWin;
    public TextMeshProUGUI textWinReward;
    public TextMeshProUGUI textGold;
    public override void Init()
    {
        btnOk.onClick.AddListener(()=>{
            mUI.Instance.HidePanel<EndPanel>();
            mUI.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
            mGameLevel.Instance.ClearInfo();
        });
    }
    public void InitInfo(bool isWin,int gold){
        if(isWin){
            textWin.text="胜利";
            textWinReward.text="获得胜利奖励";
        }else
        {
            textWin.text="失败";
            textWinReward.text="获得失败奖励";
        }
        textGold.text="$"+gold;
        mGameData.Instance.playerData.goldMoney+=gold;
        mGameData.Instance.SavePlayerData();
    }

    public override void ShowMe(){
        base.ShowMe();
        Cursor.lockState=CursorLockMode.None;
    }
    
}
