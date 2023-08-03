using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    static GameCamera instance;
    public static GameCamera Instance=>instance;
    public Transform target;
    public Vector3 offsetPos;
    public float bodyHeight;
    public float moveSpeed;
    public float rotationSpeed;
    Vector3 targetPos;
    Quaternion targetRotation;
    private void Awake() {
        instance=this;
    }
    void Update()
    {
        if (target != null)
        {
            targetPos = target.position + target.forward * offsetPos.z;
            targetPos += Vector3.up * offsetPos.y;
            targetPos += target.right * offsetPos.x;
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else return;
    }
    public void SetTarget(Transform playerTrans){
        target=playerTrans;
    }
}
