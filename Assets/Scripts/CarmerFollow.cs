using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// 摄像机 追随 Player而移动
/// </summary>
public class CarmerFollow : MonoBehaviour
{
    // 在 x轴、y轴，Player 在 camera未跟随其移动之前 分别可以移动的 距离
    public Single xMargin = 1f;
    public Single yMargin = 1f;
    // 在 x轴、y轴，Camera 移动的平滑性
    public Single xSmooth = 8f;
    public Single ySmooth = 8f;
    // Camera 的移动范围
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    Boolean CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    Boolean CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 保证每一帧都会调用
    void FixUpdate()
    {

    }

    void TrackPlayer()
    {
        // 先得到 camera 当前的 x、y轴的 值
        Single targetX = transform.position.x;
        Single targetY = transform.position.y;

        // 检查是否 超过设定的值，若超过，则开始滑动
        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * xSmooth);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, Time.deltaTime * ySmooth);
        }

        // Mathf.Clamp
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
