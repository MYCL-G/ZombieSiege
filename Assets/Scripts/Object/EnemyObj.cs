using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObj : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    EnemyInfo enemyInfo;
    int hp;
    public bool isDead = false;
    float frontTime = 0;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (isDead) return;
        anim.SetBool("run", agent.velocity != Vector3.zero);
        if (Vector3.Distance(transform.position, MainTowerObj.Instance.transform.position) < 5)
        {
            frontTime += Time.deltaTime;
            if (frontTime >= enemyInfo.atkOffset)
            {
                frontTime = 0;
                anim.SetTrigger("atk");
            }
        }
    }
    public void InitInfo(EnemyInfo info)
    {
        enemyInfo = info;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(enemyInfo.animator);
        hp = enemyInfo.hp;
        agent.speed = enemyInfo.moveSpeed;
        agent.angularSpeed = enemyInfo.roundSpeed;
        agent.acceleration = enemyInfo.moveSpeed / 2;
    }
    public void Wound(int dmg)
    {
        if(isDead) return;
        hp -= dmg;
        mMusic.Instance.PlaySound("Music/Wound");
        anim.SetTrigger("wound");
        if (hp <= 0) Dead();
    }
    public void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled=false;
        anim.SetBool("dead", true);
        mMusic.Instance.PlaySound("Music/dead");
        mGameData.Instance.playerData.goldMoney+=20;
        mUI.Instance.GetPanel<GamePanel>().UpdateGold(mGameData.Instance.playerData.goldMoney);
        Invoke("DestoryObj", 3);
        mGameLevel.Instance.enemyObjList.Remove(this);
        if(mGameLevel.Instance.CheckOver()){
            EndPanel endPanel=mUI.Instance.ShowPanel<EndPanel>();
            endPanel.InitInfo(true,mGameLevel.Instance.playerObj.gold*2);
        }
    }
    public void DestoryObj()
    {
        Destroy(gameObject);
    }
    public void BornOver()
    {
        agent.SetDestination(MainTowerObj.Instance.transform.position);
        anim.SetBool("run", true);
    }
    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (MainTowerObj.Instance.gameObject == colliders[i].gameObject)
            {
                MainTowerObj.Instance.Wound(enemyInfo.atk);
                mMusic.Instance.PlaySound("Music/Eat");
            }
        }
    }
}
