using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// 显示 Score
/// </summary>
public class Score : MonoBehaviour
{
    public Int32 score;

    // 加分后，Player 发出了阵阵嘲讽
    private PlayerControl playerControl;
    private Int32 previousScore;

    void Awake()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        guiText.text = "Score:" + score;
        if (previousScore != score)
        {
            playerControl.StartCoroutine(playerControl.Taunt());
        }
        previousScore = score;
    }
}
