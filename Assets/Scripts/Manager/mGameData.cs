using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mGameData
{
    static mGameData instance = new mGameData();
    public static mGameData Instance => instance;
    public MusicData musicData;
    public List<HeroInfo> heroInfoList;
    public PlayerData playerData;
    public HeroInfo nowHero;
    public List<SceneInfo> sceneInfoList;
    public List<EnemyInfo> enemyInfoList;
    public List<TowerInfo> towerInfoList;
    private mGameData()
    {
        musicData = mJson.Instance.LoadData<MusicData>("MusicData");
        heroInfoList = mJson.Instance.LoadData<List<HeroInfo>>("HeroInfo");
        playerData = mJson.Instance.LoadData<PlayerData>("PlayerData");
        sceneInfoList=mJson.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        enemyInfoList=mJson.Instance.LoadData<List<EnemyInfo>>("EnemyInfo");
        towerInfoList=mJson.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }
    public void SaveMusicData()
    {
        mJson.Instance.SavaData(musicData, "MusicData");
    }
    public void SavePlayerData()
    {
        mJson.Instance.SavaData(playerData, "PlayerData");
    }

}
