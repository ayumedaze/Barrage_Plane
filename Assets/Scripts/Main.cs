using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //根据开始场景中选择的英雄 来动态创建 飞机
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();

        //根据数据中的 资源路劲 进行动态创建
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resName));
        PlayerObject playerObj = obj.AddComponent<PlayerObject>();
        playerObj.speed = info.speed * 20;
        playerObj.maxHp = 10;
        playerObj.nowHp = info.hp;
        playerObj.roundSpeed = 20;

        //更新界面上显示的血量
        GamePanel.Instance.ChangeHp(info.hp);
    }
}
