using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public UIButton btnSure;
    public UIButton btnClose;

    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(()=> {
            //确定退出
            SceneManager.LoadScene("BeginScene");
        }));

        btnClose.onClick.Add(new EventDelegate(() => {
            //直接关闭该面板
            HideMe();
        }));

        HideMe();
    }
}
