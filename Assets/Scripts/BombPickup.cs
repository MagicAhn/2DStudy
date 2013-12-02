using System;
using UnityEngine;
using System.Collections;

public class BombPickup : MonoBehaviour
{
    public AudioClip pickupClip;

    private Animator anim;
    private Boolean landed = false;

    void Awake()
    {
        anim = this.gameObject.transform.root.GetComponent<Animator>();
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
        // 如果碰到的是 Player
        if (other.tag.Equals("Player"))
        {
            // 播放 pickup 音效
            AudioSource.PlayClipAtPoint(pickupClip, this.gameObject.transform.position);
            // 增加 Player 拥有的 Bomb的数量

            // 销毁crate
            Destroy(this.gameObject.transform.root.gameObject);
        }
        else if (other.tag.Equals("ground")&&!landed)
        {
            anim.SetTrigger("Land");
            this.gameObject.transform.parent = null;
            this.gameObject.AddComponent<Rigidbody2D>();    
            landed = true;
        }
    }
}
