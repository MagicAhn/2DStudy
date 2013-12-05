using System;
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    // Player 初始生命值
    public Single health = 100f;
    // Player 受到Enemies撞击 发出的声音
    public AudioClip[] OuchClips;
    // 相撞时受到的力
    public Single hurtForce = 10f;
    // 伤害值
    public Single damageAmount = 10f;
    // 不能只要 Player和Enemy接触，每一帧都去 damage，需要一个间隔的时间，在这个时间段内，帧跳转后 不计入伤害
    public Single repeatDamagePeriod = 10f;

    // Player与Enemy相撞后，不能控制其 jump，所以需要 获得 Player对象的 PlayerControl脚本
    private PlayerControl playerControl;
    // 撞着撞着 Player就挂了,需要调用 Player对象的 Animator
    private Animator anim;
    // 血条的 Sprite Renderer
    private SpriteRenderer healthBar;
    // 血条的 localScale（局部范围）
    private Vector3 healthScale;
    // 记录上次 撞击的有效时间
    private Single lastHitTime;

    void Awake()
    {
        playerControl = this.gameObject.GetComponent<PlayerControl>();
        anim = this.gameObject.GetComponent<Animator>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        // 初始时 当然是满血满状态（O(∩_∩)O哈哈哈~）
        healthScale = healthBar.transform.localScale;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // 如果碰到的是 Enemy
        if (other.gameObject.tag.Equals("Enemy"))
        {
            // 当攻击间隔 一定的时间后，伤害计数
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                // 如果 Player的血量 大于0
                if (health > 0)
                {
                    TakeDamage(other);

                    // 重新计算 上次受伤害的时间
                    lastHitTime = Time.time;
                }
                else
                {
                    // Player 死亡
                    // Player 所有的 Collider2D的 IsTrigger设置为 True
                    Collider2D[] colliders = this.gameObject.GetComponents<Collider2D>();
                    foreach (var c in colliders)
                    {
                        c.isTrigger = true;
                    }
                    // Player 所有的 Sprite 移至到 前面 UI层
                    var sprites = GetComponents<SpriteRenderer>();
                    foreach (var spriteRenderer in sprites)
                    {
                        spriteRenderer.sortingLayerName = "UI";
                    }
                    // Player 的所有脚本禁用(不能再控制移动，死了不能再 Shoot Bazooka，也不能再扔 Bomb了)
                    GetComponent<PlayerControl>().enabled = false;
                    GetComponent<Gun>().enabled = false;
                    GetComponent<LayBombs>().enabled = false;

                    // 触发死亡动画
                    anim.SetTrigger("Die");
                }
            }
        }
    }

    private void TakeDamage(Collision2D other)
    {
        // 受到伤害
        // Player 不能继续 jump
        playerControl.jump = false;
        // 在 Player和Enemy之间 创建一个 向上的向量
        Vector3 hurtVector = this.gameObject.transform.position - other.transform.position + Vector3.up * 5f;
        // 在向量的方向上 给Player一个力
        this.gameObject.rigidbody2D.AddForce(hurtVector * hurtForce);
        // 血量减少
        health -= damageAmount;
        UpdateHealthBar();

        // Player一声惨叫
        Int32 clipIndex = UnityEngine.Random.Range(0, OuchClips.Length);
        AudioSource.PlayClipAtPoint(OuchClips[clipIndex], transform.position);
    }

    public void UpdateHealthBar()
    {
        // 更新血条（颜色与大小）
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }
}
