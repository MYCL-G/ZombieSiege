using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mUI
{
    static mUI instance = new mUI();
    public static mUI Instance => instance;
    Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    Transform canvasTrans;
    private mUI()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }
    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName)) return panelDic[panelName] as T;
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe();
        return panel;
    }
    public void HidePanel<T>(bool fade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (fade)
            {
                panelDic[panelName].HideMe(()=>{
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    mUI.Instance.panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if(panelDic.ContainsKey(panelName)) return panelDic[panelName] as T;
        return null; 
    }
}
