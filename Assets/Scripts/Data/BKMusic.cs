using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;

    private AudioSource bkAudio;
    void Awake()
    {
        instance = this;
        //得到依附在同一个对象上的 音效组件
        bkAudio = this.GetComponent<AudioSource>();

        //第一次初始化 当前是否播放  音量大小是多少
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.musicIsOpen);
        SetBKMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }

    /// <summary>
    /// 开关 背景音乐的函数
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetBKMusicIsOpen(bool isOpen)
    {
        bkAudio.mute = !isOpen;
    }

    /// <summary>
    /// 设置背景音乐的 大小
    /// </summary>
    /// <param name="value"></param>
    public void SetBKMusicValue(float value)
    {
        bkAudio.volume = value;
    }

}
