using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    public GameObject towerObj = null;
    public TowerInfo nowTowerInfo = null;
    public List<int> chooseIdList;
    void Start()
    {

    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(nowTowerInfo!=null&&nowTowerInfo.next==0) return;
        mUI.Instance.GetPanel<GamePanel>().UpdateTower(this);
    }
    private void OnTriggerExit(Collider other)
    {
        mUI.Instance.GetPanel<GamePanel>().UpdateTower(null);
    }
    public void CreateTower(int id){
        TowerInfo towerInfo=mGameData.Instance.towerInfoList[id-1];
        if(towerInfo.money>mGameData.Instance.playerData.goldMoney) return;
        mGameData.Instance.playerData.goldMoney-=towerInfo.money;
        mUI.Instance.GetPanel<GamePanel>().UpdateGold(mGameData.Instance.playerData.goldMoney);
        if(towerObj!=null){
            Destroy(towerObj);
            towerObj=null;
        }
        towerObj=Instantiate(Resources.Load<GameObject>(towerInfo.res),transform.position,Quaternion.identity);
        towerObj.GetComponent<TowerObj>().InitInfo(towerInfo);
        nowTowerInfo=towerInfo;
        if(nowTowerInfo.next!=0){
            mUI.Instance.GetPanel<GamePanel>().UpdateTower(this);
        }else{
            mUI.Instance.GetPanel<GamePanel>().UpdateTower(null);
        }
    }
}
