using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginCamera : MonoBehaviour
{
    static BeginCamera instance;
    public static BeginCamera Instance=>instance;
    Animator anim;
    private void Awake() {
        instance=this;
        anim=GetComponent<Animator>();
    }
    void Start()
    {
    }
    public void TurnLeftOver(){
        mUI.Instance.ShowPanel<ChooseHeroPanel>();
    }
    public void TurnRightOver(){
        mUI.Instance.ShowPanel<BeginPanel>();
    }
    public void TurnToLeft(bool turn){
        anim.SetBool("Turn",turn);
    }
}
