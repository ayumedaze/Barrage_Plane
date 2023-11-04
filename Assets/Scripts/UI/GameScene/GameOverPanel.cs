using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UILabel labTime;
    public UIInput inputName;
    public UIButton btnSure;

    private int endTime;
    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(()=> {
            //要把当前玩家的成绩 保存到排行榜当中
            GameDataMgr.Instance.AddRankData(inputName.value, endTime);
            //切回开始界面 重新开始游戏
            SceneManager.LoadScene("BeginScene");
        }));

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示该面板时 就应该去记录 当前的时间
        //存储的是一个秒数
        endTime = (int)GamePanel.Instance.nowTime;
        //从游戏界面得到 显示的 当前时间
        labTime.text = GamePanel.Instance.labTime.text;
    }
}
