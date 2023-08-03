using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSet;
    public Button btnAbout;
    public Button btnExit;

    public override void Init()
    {
        btnStart.onClick.AddListener(()=>{
            mUI.Instance.HidePanel<BeginPanel>();
            BeginCamera.Instance.TurnToLeft(true);
        });
        btnSet.onClick.AddListener(()=>{
            mUI.Instance.ShowPanel<SetPanel>();
        });
        btnAbout.onClick.AddListener(()=>{
            
        });
        btnExit.onClick.AddListener(()=>{
            Application.Quit();
        });
    }
}
