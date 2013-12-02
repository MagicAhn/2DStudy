using System;
using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    // 爆炸范围
    public Single bombRadius = 10f;
    // 爆炸半径
    public Single bombForce = 100f;
    // 爆炸音效
    public AudioClip boom;
    // 引燃音效
    public AudioClip fuse;
    // 引燃时间
    public Single fuseTime = 1.5f;
    // 爆炸效果
    public GameObject explosion;

    private ParticleSystem explosionFX;

    void Awake()
    {
        explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start()
    {
        if (this.gameObject.transform.root == this.gameObject.transform)
        {
            StartCoroutine(BombDetonation());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BombDetonation()
    {
        // 播放 引燃音效
        AudioSource.PlayClipAtPoint(fuse, this.gameObject.transform.position);

        yield return new WaitForSeconds(fuseTime);

        Explode();
    }

    void Explode()
    {
        // 如果 player 有炸弹，表示 还没有扔炸弹，可以随时扔了

        // 开始 下一个 延时加载生产 Pickup

        // 计算 爆炸范围内的 Enemy层上所有的 Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, bombRadius, 1 << LayerMask.NameToLayer("Enemies"));

        foreach (var enemyCollider in colliders)
        {
            Rigidbody2D rb = enemyCollider.rigidbody2D;
            if (rb.tag.Equals("Enemy") && rb != null)
            {
                // 先找到 Enemy Script Component，将 HP 设为 0
                rb.gameObject.GetComponent<Enemy>().HP = 0;
                // 计算 Bomb 和 Enemy之间的距离
                Vector3 deltaPos = this.gameObject.transform.position - rb.transform.position;
                // 在 deltaPos的 方向上 给 Enemy一个 BombForce
                Vector3 force = deltaPos.normalized * bombForce;
                rb.AddForce(force);
            }
        }

        // 设置 Particle System的 位置并播放
        explosionFX.transform.position = this.gameObject.transform.position;
        explosionFX.Play();

        // 实例化 一个 explosion的 Prefab (一个圆圈)
        Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);

        // 爆炸音效
        AudioSource.PlayClipAtPoint(boom, this.gameObject.transform.position);

        // 销毁 Bomb
        Destroy(gameObject);
    }
}
