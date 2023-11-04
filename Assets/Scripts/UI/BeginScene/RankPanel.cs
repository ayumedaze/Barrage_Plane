using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnClose;
    public UIScrollView svList;

    //专门用于存储 下面的单条数据控件的
    private List<RankItem> itemList = new List<RankItem>();

    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(()=> {
            HideMe();
        }));

        HideMe();

        print(Application.persistentDataPath);

        //测试代码 一开始 加几条排行榜数据进去 用于更新 不然一开始游戏没有数据
        //for (int i = 0; i < 2; i++)
        //{
        //    GameDataMgr.Instance.AddRankData("唐老狮" + i, Random.Range(40, 60));
        //}
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //更新面板上显示的信息

        //获取本地存储的排行榜数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.rankList;
        //根据数据 更新面板上 组合控件的信息 
        //组合控件数量只会增加 不会减少 因为玩家只会玩游戏 增加数据 不会删除数据
        for (int i = 0; i < list.Count; i++)
        {
            //如果面板上 已经存在 组合控件 直接更新即可
            if(itemList.Count > i)
            {
                itemList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            //如果面板上 组合控件 不够多 就去实例化出来
            else
            {
                //创建预设体
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                //设置父对象
                obj.transform.SetParent(svList.transform, false);
                //设置位置
                obj.transform.localPosition = new Vector3(0, 115 - i * 45, 0);

                //设置数据
                //得到脚本
                RankItem item = obj.GetComponent<RankItem>();
                //调用设置数据的方法
                item.InitInfo(i + 1, list[i].name, list[i].time);
                //记录
                itemList.Add(item);
            }
        }
    }
}
