using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObj : MonoBehaviour
{
    static MainTowerObj instance;
    public static MainTowerObj Instance=>instance;
    int hp;
    int maxHp;
    bool isDead=false;
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void UpdateHp(int hp,int maxHp){
        this.hp=hp;
        this.maxHp=maxHp;
        mUI.Instance.GetPanel<GamePanel>().UpdateHp(hp,maxHp);
    }
    public void Wound(int dmg){
        if(isDead) return;
        hp-=dmg;
        if(hp<=0){
            hp=0;
            isDead=true;
            EndPanel endPanel=mUI.Instance.ShowPanel<EndPanel>();
            endPanel.InitInfo(false,maxHp/2);
        }
        UpdateHp(hp,maxHp);
    }
    private void OnDestroy() {
        instance=null;
    }
}
