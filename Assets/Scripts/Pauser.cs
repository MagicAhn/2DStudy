using System;
using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour
{
    private Boolean paused = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            
            // timeScale(时间刻度),若设置为 0，游戏基本上设置为 暂停
            // 若为1， 游戏以正常速度 运行
            Time.timeScale = paused ? 0 : 1.0f;
        }
    }
}
