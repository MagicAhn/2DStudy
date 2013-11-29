using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 爆炸效果
    void OnExplode()
    {
        // 在 z轴 旋转一个任意的角度
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        // 实例化 Explosion对象
        Instantiate(explosion, transform.position, randomRotation);
    }


    // MonoBehaviour.OnTriggerEnter2D(Collider2D col)
    void OnTriggerEnter2D(Collider2D col)
    {
        // 如果 碰到的是 Enemy对象
        if (col.tag.Equals("enemy"))
        {
            // 找到 Enemy对象的 Enemy脚本，并调用 Hurt方法
            col.gameObject.GetComponent<Enemy>().Hurt();
            // 爆炸效果
            OnExplode();
            // 销毁 rocket
            Destroy(this.gameObject);
        }
        else if (col.tag.Equals("BombPickup"))
        {
            OnExplode();
            Destroy(this.gameObject);
        }
        else if (!col.tag.Equals("Player"))
        {
            OnExplode();
            Destroy(this.gameObject);
        }
    }
}
