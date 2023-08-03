using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;
    public Image imgPlace;
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textAbout;
    int nowIndex=0;
    SceneInfo nowSceneInfo;
    public override void Init()
    {
        ChangeScene();
        btnLeft.onClick.AddListener(()=>{
            nowIndex--;
            if(nowIndex<0) nowIndex=mGameData.Instance.sceneInfoList.Count-1;
            ChangeScene();
        });
        btnRight.onClick.AddListener(()=>{
            nowIndex++;
            if(nowIndex>mGameData.Instance.sceneInfoList.Count-1) nowIndex=0;
            ChangeScene();
        });
        btnStart.onClick.AddListener(()=>{
            mUI.Instance.HidePanel<ChooseScenePanel>();
            AsyncOperation ao=SceneManager.LoadSceneAsync(nowSceneInfo.sceneName);
            ao.completed+=((obj)=>{
                mGameLevel.Instance.InitInfo(nowSceneInfo);
            });
        });
        btnBack.onClick.AddListener(()=>{
            mUI.Instance.HidePanel<ChooseScenePanel>();
            mUI.Instance.ShowPanel<ChooseHeroPanel>();

        });
    }
    public void ChangeScene(){
        nowSceneInfo=mGameData.Instance.sceneInfoList[nowIndex];
        textTitle.text=nowSceneInfo.name;
        textAbout.text=nowSceneInfo.tips;
        imgPlace.sprite=Resources.Load<Sprite>(nowSceneInfo.imgRes);
    }
}
