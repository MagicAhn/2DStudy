using System;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    // 每次 Spawn 间隔时间
    public Single spawnTime = 5f;
    // 游戏开始后 首次 Spawn 的延时
    public Single spawnDelay = 2f;
    // 可产生 哪些 Enemy
    public GameObject[] enemies;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        // 实例化一个 Enemy
        Int32 index = UnityEngine.Random.Range(0, enemies.Length);
        Instantiate(enemies[index], this.gameObject.transform.position, this.gameObject.transform.rotation);

        // 播放 Particle 
        var particles = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particles)
        {
            particle.Play();
        }
    }
}
