using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObj : MonoBehaviour
{
    public Transform head;
    public Transform[] firePoints;
    float roundSpeed = 20;
    TowerInfo towerInfo;
    EnemyObj target;
    float nowtime;
    Vector3 enemyPos;
    void Update()
    {
        nowtime += Time.deltaTime;
        if (towerInfo.type == 1)
        {
            if (target == null ||
                target.isDead ||
                Vector3.Distance(transform.position, target.transform.position) > towerInfo.atkRange)
            {
                target = mGameLevel.Instance.FindEnemy(transform.position, towerInfo.atkRange);
            }
            if (target != null && !target.isDead)
            {
                enemyPos = target.transform.position;
                enemyPos.y = head.position.y;
                head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(enemyPos - head.position), roundSpeed * Time.deltaTime);
                if (Vector3.Angle(head.forward, enemyPos - head.position) < 5 && nowtime > towerInfo.offsetTime)
                {
                    target.Wound(towerInfo.atk);

                    mMusic.Instance.PlaySound("Music/Tower");
                    nowtime = 0;
                }
            }
        }
        if (towerInfo.type == 2)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, towerInfo.atkRange/2, 1 << LayerMask.NameToLayer("Enemy"));
            // mMusic.Instance.PlaySound("Music/Knife");
            for (int i = 0; i < colliders.Length; i++)
            {
                EnemyObj enemyObj = colliders[i].gameObject.GetComponent<EnemyObj>();
                if (enemyObj != null) enemyObj.Wound(towerInfo.atk);
            }
        }
    }
    public void InitInfo(TowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
    }
}
