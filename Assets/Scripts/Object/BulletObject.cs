using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    //子弹使用的数据
    private BulletInfo info;

    //用于曲线移动的 计时变量
    private float time;

    //初始化子弹数据的方法
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        //根据生命周期函数 决定自己什么时候 延迟移除
        //Destroy(this.gameObject, info.lifeTime);
        Invoke("DealyDestroy", info.lifeTime);
    }

    private void DealyDestroy()
    {
        //直接执行死亡 会播放 特效
        Dead();
    }

    //销毁场景上的子弹
    public void Dead()
    {
        //创建死亡特效
        GameObject effObj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        //设置特效的位置 创建在当前子弹的位置
        effObj.transform.position = this.transform.position;
        //1秒后延迟移除特效对象
        Destroy(effObj, 1f);

        //销毁子弹对象
        Destroy(this.gameObject);
    }

    //和对象碰撞时（触发）
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //得到玩家脚本
            PlayerObject obj = other.GetComponent<PlayerObject>();
            //玩家受伤减血
            obj.Wound();

            //销毁自己 
            Dead();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //所有移动的共同特点 都是朝自己的面朝向动
        this.transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);
        //接着再来处理 其它的移动逻辑
        //1 代表 只朝自己面朝向移动 直线运动
        //2 代表 曲线运动 
        //3 代表 右抛物线
        //4 代表 左抛物线
        //5 代表 跟踪导弹
        switch (info.type)
        {
            case 2:
                time += Time.deltaTime;
                //sin里面值变化的快慢 决定了 左右变化的频率
                //乘以的速度 变化的大小 决定了 左右位移的多少
                //曲线运动时 roundSpeed旋转速度 主要用于控制 变化的频率
                this.transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed);
                break;
            case 3:
                //右抛物线 无非 就是 去改变 旋转角度
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                //左抛物线 无非 就是 去改变 旋转角度
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                //跟踪移动 不停的计算 玩家和我之间的方向向量 然后得到四元数 然后自己的角度 不停的 变化为这个目标四元数
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                           Quaternion.LookRotation(PlayerObject.Instance.transform.position - this.transform.position),
                                                           info.roundSpeed * Time.deltaTime);
                break;
        }
    }
}
