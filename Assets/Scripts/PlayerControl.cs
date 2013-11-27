using System;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public Boolean facingRight = true;
    [HideInInspector]
    public Boolean jump = false;

    public Single moveForce = 365f;
    public Single maxSpeed = 5f;
    public Single jumpForce = 1000f;
    public AudioClip[] jumpClips;
    public AudioClip[] taunts;
    public Single tauntProbability = 50f;
    public Single tauntDelay = 1f;

    private Transform groundCheck;
    public Animator anim;
    // 判断是否着陆
    private Boolean grounded = false;

    void Awake()
    {
        // 标记 player 着地的 坐标
        groundCheck = this.gameObject.transform.Find("groundCheck");
        // 得到 player 的 Animator
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 场景中 PlateformTop、PlateformBridge、PlateformUfo 的 Layer 位 ground
        grounded = Physics2D.Linecast(this.gameObject.transform.position, groundCheck.position,
            1 << LayerMask.NameToLayer("Ground"));

        // 着陆 并且按了 “Jump”
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // jump 后 
        if (jump)
        {
            // 触犯 Jump 动画
            anim.SetTrigger("Jump");

            // 向上 加力 
            this.gameObject.rigidbody2D.AddForce(new Vector2(0f, jumpForce));

            // 播放一段音频
            Int32 index = new Random().Next(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[index],transform.position);

            // 加完力后，把 jump 设置为 false
            jump = false;
        }

        // hero 移动
        // 水平方向输入 的值 [-1,1],1表示 x轴正方向，-1表示 x轴负方向
        Single horizontal = Input.GetAxis("Horizontal");
        // 给附加到 gameObejct的 Animator对象中的 Speed参数 赋值
        anim.SetFloat("Speed", Mathf.Abs(horizontal));

        // 先 加速度 然后到 恒定速度 的过程
        // 先加力
        if (this.gameObject.rigidbody2D.velocity.x * horizontal < maxSpeed)
        {
            this.gameObject.rigidbody2D.AddForce(Vector2.right * horizontal * moveForce);
        }
        // 在决定速度
        if (Mathf.Abs(this.gameObject.rigidbody2D.velocity.x) > maxSpeed)
        {
            this.gameObject.rigidbody2D.velocity = new Vector2(Mathf.Sign(this.gameObject.rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }

        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = this.gameObject.transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

