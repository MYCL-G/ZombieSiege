using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    public int maxWave;
    public int enemyOneWave;
    int nowNum;
    public List<int> enemyIdList;
    int nowEnemyId;
    public float createOffsetTime;
    public float delayTime;
    public float firstDelayTime;
    void Start()
    {
        Invoke("CreateWave",firstDelayTime);
        mGameLevel.Instance.AddEnemyPoint(this);
        mGameLevel.Instance.UpdateMaxNum(maxWave);
    }

    void CreateWave(){
        nowEnemyId=enemyIdList[Random.Range(0,enemyIdList.Count)];
        nowNum=enemyOneWave;
        maxWave--;
        mGameLevel.Instance.ChangeNowWaveNum(1);
        CreateEnemy();
    }
    void CreateEnemy(){
        EnemyInfo enemyInfo=mGameData.Instance.enemyInfoList[nowEnemyId];
        GameObject obj=Instantiate(Resources.Load<GameObject>(enemyInfo.res),transform.position,Quaternion.identity);
        EnemyObj enemyObj=obj.AddComponent<EnemyObj>();
        enemyObj.InitInfo(enemyInfo);
        mGameLevel.Instance.enemyObjList.Add(enemyObj);
        nowNum--;
        if(nowNum>0){
            Invoke("CreateEnemy",createOffsetTime);
        }
        else{
            if(maxWave>0)
            Invoke("CreateWave",delayTime);
        }
    }
    public bool CheckOver(){
        return nowNum==0&&maxWave==0;
    }
}
