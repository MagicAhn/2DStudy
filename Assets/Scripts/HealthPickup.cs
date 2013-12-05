using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// healthCrate着地后， parachute就消失了，只能捡起health
/// Script 附加到 health 上
/// Animator 附加到 healthCrate 上
/// </summary>
public class HealthPickup : MonoBehaviour
{
    // Health crates 给Player加多少血
    // Mathf.Clamp函数
    public Single healthBonus;
    // Health crates 被捡起来的声音
    public AudioClip collect;

    // Health crates 被捡起后，延时调用 PickupSpawner的 DeliveryPickup
    private PickupSpawner pickupSpawner;
    // healthCrate 的 Animator
    private Animator anim;
    private Boolean grounded = false;

    void Awake()
    {
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
        anim = gameObject.transform.root.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰到 Player
        if (other.gameObject.tag.Equals("Player"))
        {
            // 获取 Player的 PlayerHealth脚本，加血，并在[0f,100f]取一个值
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.health += healthBonus;
            playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

            // 更新血条
            playerHealth.UpdateHealthBar();

            // 延时调用下一个 delivery(逻辑上 DeliverPickup是 PickupSpawner的方法，就有PickupSpawner对象去调用)
            pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

            // 播放一个 Collect的 音效
            AudioSource.PlayClipAtPoint(collect, transform.position);

            // 销毁 HealthPickup对象
            Destroy(transform.root.gameObject);
        }
        else if (other.gameObject.tag.Equals("ground") && !grounded)
        {
            // 调用动画
            anim.SetTrigger("Land");
            gameObject.transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            grounded = true;
        }
    }
}
