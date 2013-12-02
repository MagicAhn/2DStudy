using System;
using UnityEngine;
using System.Collections;

public class LayBombs : MonoBehaviour
{
    // 有没有现在 已经被 放下的 Bomb
    [HideInInspector]
    public Boolean bombLaied = false;
    // Player 拥有的 Bomb(包括被抛到空中还没着地的)
    public Int32 bombCount = 0;
    public AudioClip bombsAway;
    // Bomb 的 Prefab
    public GameObject bomb;

    // 用来表示 Player 是否拥有 Bomb
    private GUITexture bombHUD;

    void Awake()
    {
        bombHUD = GameObject.Find("ui_bombHUD").guiTexture;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && bombCount > 0 && !bombLaied)
        {
            bombCount--;
            bombLaied = true;
            AudioSource.PlayClipAtPoint(bombsAway, this.gameObject.transform.position);

            Instantiate(bomb, this.gameObject.transform.position, transform.rotation);
        }

        // GUI中的 bombHUD 只有当 Player 拥有 Bomb时的时候显示
        bombHUD.enabled = bombCount > 0;
    }
}
