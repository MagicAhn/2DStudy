using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

/// <summary>
/// 通过延迟加载 分别生产 Bus，Cab，Swan
/// </summary>
public class BackgroundPropSpawner : MonoBehaviour
{
    // 选择 Bus等对应的 Rigidbody2D的 Prefabs
    public Rigidbody2D backgroundProf;
    public Single minTimeBetweenSpawns;
    public Single maxTimeBetweenSpawns;
    // Prefabs 的最左和最右的 x轴边界
    public Single rightSpawnPosX;
    public Single leftSpawnPosX;
    // Prefabs 的最高和最低的 y轴边界
    public Single maxSpawnPosY;
    public Single minSpawnPosY;
    // Prefabs 的最高和最低的 速率
    public Single minSpeed;
    public Single maxSpeed;

    // Use this for initialization
    void Start()
    {
        // 设置随机产生 Prefabs 的数量
        Random.seed = System.DateTime.Now.Millisecond;

        // 延时加载(通过 方法名 或者 调用方法 皆可)
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        // 在一个 Prefab 被实例化之前，先让其等待一个随机的 时间
        // 最短 和 最长的 产生时间(在 Inspector中设定)
        Single waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        yield return new WaitForSeconds(waitTime);

        // 让 Prefabs 在 BackgroundAnimation中 移动
        // 确定一个移动的方向 (一开始Prefabs 中的 每个 Prefab都是向左的)
        Boolean facingLeft = Random.Range(0, 2) == 0;
        // 确定移动的范围
        Single posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
        Single posY = Random.Range(minSpawnPosY, maxSpawnPosY);
        // 确定 Prefab的 初始位置（向量）
        Vector3 spawnPos = new Vector3(posX, posY, this.gameObject.transform.position.z);
        // 实例化 Prefab 为一个 Rigidbody2D 对象 (Quaternion.identity 表示没有旋转)
        Rigidbody2D propInstance = Instantiate(backgroundProf, spawnPos, Quaternion.identity) as Rigidbody2D;
        if (!facingLeft)
        {
            // 翻转 Prefab
            Flip(propInstance);
        }
        // 给 Rigidbody2D对象 一个速度（向量）
        Single speed = Random.Range(minSpeed, maxSpeed);
        speed *= facingLeft ? -1 : 1;
        propInstance.velocity = new Vector2(speed, 0);

        // 继续 延时加载一个 Prefab
        StartCoroutine(Spawn());



        yield return null;
    }

    private void Flip(Rigidbody2D instatnce)
    {
        Vector3 theScale = instatnce.transform.localScale;
        theScale.x *= -1;
        instatnce.transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
