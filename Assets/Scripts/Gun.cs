using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// 点击 Shoot 发射导弹，从 Bazooka口的位置 水平方向 给一个力
/// 添加 粒子效果(导弹尾部火焰)(在模型中做好)
/// --碰到 敌人后 敌人和导弹消灭 产生爆炸效果，碰到 墙壁后 产生 爆炸效果
/// 。。。
/// </summary>
public class Gun : MonoBehaviour
{
    // 从 Prefabs 中 实例化 出的 rocket对象
    public Rigidbody2D rocket;
    public Single speed = 20f;

    // 当发射 导弹的时候， Hero 需要做一个相应的动作，这就需要获得 他的 Animator
    // 如何决定 导弹发射的方向 ，Hero的Component--- PlayerControl的 facingRight属性，所以还需要得到 PlayControl
    private PlayerControl playerControl;
    private Animator anim;

    void Awake()
    {
        playerControl = this.gameObject.transform.root.GetComponent<PlayerControl>();

        anim = this.gameObject.transform.root.gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");

            audio.Play();

            if (playerControl.facingRight)
            {
                var bulletInstance = Instantiate(rocket, this.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as
     Rigidbody2D;

                bulletInstance.velocity = new Vector2(speed,0);
            }
            else if (!playerControl.facingRight)
            {
                var bulletInstance = Instantiate(rocket, this.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as
     Rigidbody2D;

                bulletInstance.velocity = new Vector2(-speed, 0);
            }
        }
    }
}
