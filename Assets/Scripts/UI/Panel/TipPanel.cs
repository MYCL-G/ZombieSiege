using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipPanel : BasePanel
{
    public Button btnClose;
    public TextMeshProUGUI textTip;
    public override void Init()
    {
        btnClose.onClick.AddListener(()=>{
            mUI.Instance.HidePanel<TipPanel>();
        });
    }
    public void ChangeTip(string tip){
        textTip.text=tip;
    }
}
