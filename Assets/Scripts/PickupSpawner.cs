using System;
using System.Threading;
using UnityEngine;
using System.Collections;

/// <summary>
/// Delivery Bomb crates 和 Health crates
/// 逻辑：一开始 延时 delivery 一个 crates
/// 当是 Health crates，只有当 Player 捡到其时，才会继续 延时调用 DeliverPickup方法
/// 在 HealthPickup的 OnTriggerEnter2D中调用
/// 当时 Bomb crates，只有当 Bomb 被Player扔掉 爆炸后，才会继续 延时调用 DeliverPickup方法
/// 在 Bomb的 Explode中调用 
/// </summary>
public class PickupSpawner : MonoBehaviour
{
    // 有两种 Prefabs：Bomb和Health
    public GameObject[] pickups;
    // Delivery 发生的 x轴的 最大和最小值
    public Single dropRangeRight;
    public Single dropRangeLeft;
    // Delivery 的延时
    public Single pickupDeliveryTime = 5f;
    // Player 的最大生命值，超过这个值， 只有 Bomb crates可以被 delivery
    public Single highHealthThreshold = 75f;
    // Player 的最小生命值，小于这个值， 只有 Health crates可以被 delivery
    public Single lowHealthThreshold = 25f;

    private PlayerHealth playerHealth;

    void Awake()
    {
        // 获取存有 Player 当前生命值的 游戏脚本
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Use this for initialization
    void Start()
    {
        // 第一次 Delivery
        StartCoroutine(DeliverPickup());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator DeliverPickup()
    {
        yield return new WaitForSeconds(pickupDeliveryTime);
        Single dropPosX = UnityEngine.Random.Range(dropRangeLeft, dropRangeRight);
        // drop的位置
        Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);
        // Delivery 选择的逻辑
        if (playerHealth.health > highHealthThreshold)
        {
            Instantiate(pickups[0], dropPos, Quaternion.identity);
        }
        else if (playerHealth.health < lowHealthThreshold)
        {
            Instantiate(pickups[1], dropPos, Quaternion.identity);
        }
        else
        {
            Int32 pickupIndex = UnityEngine.Random.Range(0, pickups.Length);
            Instantiate(pickups[pickupIndex], dropPos, Quaternion.identity);
        }
    }
}
