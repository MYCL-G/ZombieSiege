using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ChooseHeroPanel : BasePanel
{
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textPrice;
    public Button btnStart;
    public Button btnBack;
    public Button btnLeft;
    public Button btnRight;
    public Button btnUnlock;
    Transform heroPos;
    GameObject heroObj;
    HeroInfo nowHeroInfo;
    int nowIndex = 0;
    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;
        textMoney.text = mGameData.Instance.playerData.goldMoney.ToString();
        ChangeHero();
        btnStart.onClick.AddListener(() =>
        {
            mGameData.Instance.nowHero=nowHeroInfo;
            mUI.Instance.HidePanel<ChooseHeroPanel>();
            mUI.Instance.ShowPanel<ChooseScenePanel>();
        });
        btnBack.onClick.AddListener(() =>
        {
            mUI.Instance.HidePanel<ChooseHeroPanel>();
            BeginCamera.Instance.TurnToLeft(false);
        });
        btnLeft.onClick.AddListener(() =>
        {
            nowIndex--;
            if (nowIndex < 0) nowIndex = mGameData.Instance.heroInfoList.Count - 1;
            ChangeHero();
        });
        btnRight.onClick.AddListener(() =>
        {
            nowIndex++;
            if (nowIndex >= mGameData.Instance.heroInfoList.Count) nowIndex = 0;
            ChangeHero();
        });
        btnUnlock.onClick.AddListener(() =>
        {
            if(mGameData.Instance.playerData.goldMoney>=nowHeroInfo.lockMoney){
                mGameData.Instance.playerData.goldMoney-=nowHeroInfo.lockMoney;
                textMoney.text = mGameData.Instance.playerData.goldMoney.ToString();
                mGameData.Instance.playerData.huyHero.Add(nowHeroInfo.id);
                mUI.Instance.ShowPanel<TipPanel>().ChangeTip("购买成功");
                mGameData.Instance.SavePlayerData();
                UpdateBtnUnlock();
            }else{
                mUI.Instance.ShowPanel<TipPanel>().ChangeTip("金钱不足");;
            }
        });
    }
    void ChangeHero()
    {
        nowHeroInfo = mGameData.Instance.heroInfoList[nowIndex];
        textName.text=nowHeroInfo.tips;
        if (heroObj != null) Destroy(heroObj.gameObject);
        heroObj = Instantiate(Resources.Load<GameObject>(nowHeroInfo.res), heroPos.position, heroPos.rotation);
        Destroy(heroObj.GetComponent<PlayerObj>());
        UpdateBtnUnlock();
    }
    void UpdateBtnUnlock()
    {
        if (nowHeroInfo.lockMoney > 0 && !mGameData.Instance.playerData.huyHero.Contains(nowHeroInfo.id))
        {
            btnUnlock.gameObject.SetActive(true);
            textPrice.text = "$" + nowHeroInfo.lockMoney;
            btnStart.gameObject.SetActive(false);
        }else{
            btnStart.gameObject.SetActive(true);
            btnUnlock.gameObject.SetActive(false);
        }
    }
    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if(heroObj!=null){
            DestroyImmediate(heroObj);
            heroObj=null;
        }
    }
}
