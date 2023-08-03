using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    int atk;
    public int gold;
    float roundSpeed = 100;
    Animator anim;
    public Transform[] firePoints;
    int nowFirePoint = 0;
    Transform firePoint;
    public void InitPlayerInfo(int atk, int gold)
    {
        this.atk = atk;
        this.gold = gold;
        UpdateGold();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("hSpeed", Input.GetAxis("Horizontal"));
        anim.SetFloat("vSpeed", Input.GetAxis("Vertical"));
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift)) anim.SetTrigger("Roll");
        if (Input.GetMouseButton(0)) anim.SetTrigger("Attack");

        if (Input.GetKey(KeyCode.LeftControl)) anim.SetLayerWeight(1, 1);
        else if (Input.GetKeyUp(KeyCode.LeftControl)) anim.SetLayerWeight(1, 0);

    }
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("Enemy"));
        mMusic.Instance.PlaySound("Music/Knife");
        for (int i = 0; i < colliders.Length; i++)
        {
            EnemyObj enemyObj = colliders[i].gameObject.GetComponent<EnemyObj>();
            if (enemyObj != null) enemyObj.Wound(atk);
        }
    }
    public void ShootEvent()
    {
        firePoint = firePoints[nowFirePoint];
        nowFirePoint++;
        if (nowFirePoint >= firePoints.Length) nowFirePoint = 0;
        RaycastHit[] hits = Physics.RaycastAll(new Ray(firePoint.position, firePoint.forward), 1000, 1 << LayerMask.NameToLayer("Enemy"));
        mMusic.Instance.PlaySound("Music/Gun");
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyObj enemyObj = hits[i].collider.gameObject.GetComponent<EnemyObj>();
            if (enemyObj != null&&!enemyObj.isDead)
            {
                enemyObj.Wound(atk);
                break;
            }
        }
    }
    public void UpdateGold()
    {
        mUI.Instance.GetPanel<GamePanel>().UpdateGold(gold);
    }
    public void AddGold(int gold)
    {
        this.gold += gold;
        UpdateGold();
    }
}
