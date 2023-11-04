using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //音乐相关数据
    public MusicData musicData;
    //排行榜数据
    public RankData rankData;
    //角色数据
    public RoleData roleData;
    //子弹数据
    public BulletData bulletData;
    //开火点 数据
    public FireData fireData;

    //当前选择的角色 编号
    public int nowSelHeroIndex = 0;

    private GameDataMgr()
    {
        //就是获取本地硬盘中存储的 音乐数据
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        //一开始 就读取本地的 排行榜数据
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        //一开始 就读取 角色数据 
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        //一开始 就读取 子弹数据
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        //一开始 就读取 开火点数据
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;
    }

    #region 音乐音效相关
    //保存音乐相关数据的方法
    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    //开关背景音乐的方法
    public void SetMusicIsOpen(bool isOpen)
    {
        //改数据
        musicData.musicIsOpen = isOpen;
        //真正改变背景音乐的开关
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);
    }

    //开关音效的方法
    public void SetSoundIsOpen(bool isOpen)
    {
        //改数据
        musicData.SoundIsOpen = isOpen;
        //真正改变音效的开关
    }

    //设置背景音乐的音效 0~1
    public void SetMusicValue(float value)
    {
        //改数据
        musicData.musicValue = value;
        //真正的改变背景音乐的大小
        BKMusic.Instance.SetBKMusicValue(value);
    }

    //设置音效的音效 0~1
    public void SetSoundValue(float value)
    {
        //改数据
        musicData.soundValue = value;
    }
    #endregion

    #region 排行榜相关
    /// <summary>
    /// 添加排行榜数据
    /// </summary>
    /// <param name="name">玩家名</param>
    /// <param name="time">通关时间</param>
    public void AddRankData(string name, int time)
    {
        //单条数据
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);

        //排序
        rankData.rankList.Sort((a, b)=>
        {
            if (a.time > b.time)
                return -1;
            return 1;
        });

        //移除大于20条的内容
        if (rankData.rankList.Count > 20)
            rankData.rankList.RemoveAt(20);
        //rankData.rankList.RemoveRange(20, rankData.rankList.Count - 20);

        //保存数据
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    #endregion


    #region 玩家数据相关
    /// <summary>
    /// 提供给外部 获取当前选择的 英雄数据
    /// </summary>
    /// <returns></returns>
    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
    #endregion
}
