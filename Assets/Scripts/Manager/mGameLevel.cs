using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mGameLevel
{
    static mGameLevel instance = new mGameLevel();
    public static mGameLevel Instance => instance;
    public PlayerObj playerObj;
    List<EnemyPoint> enemyPointList = new List<EnemyPoint>();
    public List<EnemyObj> enemyObjList = new List<EnemyObj>();
    int nowWaveNum = 0;
    int maxWaveNum = 0;
    private mGameLevel()
    {
    }
    public void InitInfo(SceneInfo sceneInfo)
    {
        mUI.Instance.ShowPanel<GamePanel>();
        HeroInfo heroInfo = mGameData.Instance.nowHero;
        Transform heroBornPos = GameObject.Find("HeroBornPos").transform;
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(heroInfo.res), heroBornPos.position, heroBornPos.rotation);
        GameCamera.Instance.SetTarget(heroObj.transform);
        playerObj = heroObj.GetComponent<PlayerObj>();
        playerObj.InitPlayerInfo(heroInfo.atk, sceneInfo.gold);
        mUI.Instance.ShowPanel<GamePanel>();
        MainTowerObj.Instance.UpdateHp(sceneInfo.towerHp, sceneInfo.towerHp);
    }
    public void AddEnemyPoint(EnemyPoint enemyPoint)
    {
        enemyPointList.Add(enemyPoint);
    }
    public bool CheckOver()
    {
        for (int i = 0; i < enemyPointList.Count; i++)
        {
            if (!enemyPointList[i].CheckOver()) return false;
        }
        if (enemyObjList.Count > 0) return false;
        return true;
    }
    public void UpdateMaxNum(int num)
    {
        maxWaveNum = +num;
        nowWaveNum = maxWaveNum;
        mUI.Instance.GetPanel<GamePanel>().UpdateWave(nowWaveNum, maxWaveNum);
    }
    public void ChangeNowWaveNum(int num)
    {
        nowWaveNum -= num;
        mUI.Instance.GetPanel<GamePanel>().UpdateWave(nowWaveNum, maxWaveNum);
    }
    public void ClearInfo()
    {
        enemyPointList.Clear();
        enemyObjList.Clear();
        nowWaveNum = 0;
        maxWaveNum = 0;
        playerObj = null;
    }
    public EnemyObj FindEnemy(Vector3 pos, int range)
    {
        for (int i = 0; i < enemyObjList.Count; i++)
        {
            if (Vector3.Distance(pos, enemyObjList[i].transform.position) < range && !enemyObjList[i].isDead)
            {
                return enemyObjList[i];
            }
        }
        return null;
    }
}
