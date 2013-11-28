using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Single moveSpeed = 2f;
    public Int32 HP = 2;

    // Enemy2 需要受两次攻击 才会死亡，第一个 Damaged，第二次 Dead
    public Sprite deadEnemy;
    public Sprite damagedEnemy;

    // Enemy 死亡时 让其发生一定的旋转，理想的办法是 加一个 扭矩 Torque
    public Single deathSpinMin = -100f;
    public Single deathSpinMax = 100f;

    // 播放 Enemy 死亡时的音效
    public AudioClip[] deathClips;

    // Enemy 正方向前有一个 frontCheck
    private Transform frontCheck;
    // Enemy body的 SpriteRenderer
    private SpriteRenderer bodyRenderer;
    // Enemy 死亡状态
    private Boolean dead = false;

    void Awake()
    {
        frontCheck = this.gameObject.transform.Find("frontCheck").transform;
        bodyRenderer = this.gameObject.transform.Find("body").GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Enemy 的移动速度
        this.gameObject.rigidbody2D.velocity = new Vector2(this.gameObject.transform.localScale.x * moveSpeed, this.gameObject.rigidbody2D.velocity.y);

        // Enemy的 frontCheck前方 1位置的所有 可以碰撞检测器
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

        foreach (var frontHit in frontHits)
        {
            // 如果 碰到 Tag为 Obstacle的 碰撞检测器
            if (frontHit.tag.Equals("Obstacle"))
            {
                // 翻转物体
                Flip();

                break;
            }
        }

        // 如果 Enemy的 血量为 1，并且有 对应的 Damaged Sprite，则把 Enemy body上的 SpriteRenderer 中的 Sprite换成 damagedEnemy
        if (HP == 1 && damagedEnemy != null)
        {
            bodyRenderer.sprite = damagedEnemy;
        }
        // Enemy 死亡
        if (HP <= 0 && !dead)
        {
            Death();
        }
    }

    private void Flip()
    {
        Vector3 theScale = this.gameObject.transform.localScale;
        theScale.x *= -1;
        this.gameObject.transform.localScale = theScale;
    }

    // Enemy 受到攻击，减少血量
    public void Hurt()
    {
        HP--;
    }

    // Enemy 受到攻击，死亡
    public void Death()
    {
        // 先将 Enemy 所有的 SpriteRenderer 设为 不可用
        var renderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in renderers)
        {
            spriteRenderer.enabled = false;
        }
        // 重新将 body上的 SpriteRenderer 设为 可用
        bodyRenderer.enabled = true;
        bodyRenderer.sprite = deadEnemy;

        // 设置 Enemy dead为 true
        dead = true;

        // 让 Enemy 有一个旋转（加一个 杠杆力）
        this.gameObject.rigidbody2D.fixedAngle = false;
        this.gameObject.rigidbody2D.AddTorque(UnityEngine.Random.Range(deathSpinMin, deathSpinMax));

        // A trigger doesn't collide with rigid bodies. Instead it sends OnTriggerEnter, OnTriggerExit and OnTriggerStay message when a rigidbody enters or exits the trigger.
        // 这样 Enemy就可以受 Gravity影响 掉下来了
        var colliders = this.gameObject.GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            col.isTrigger = true;
        }

        // 播放一段音效
        var clipIndex = UnityEngine.Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[clipIndex],this.gameObject.transform.position);

        // 在 Enemy头顶显示一个分数，并过一段时间消失
        Vector3 scorePos = this.gameObject.transform.position;
        scorePos.y += 1.5f;
        // 在 scorePos出实例化一个 score
    }
}
