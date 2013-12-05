using System;
using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
    // 需要被 parallaxed 的 背景
    public Transform[] background;
    // 摄像机的移动 使backgrounds 移动的比例
    public Single parallaxScale;
    // 每个连续层的 parallax
    public Single parallaxReductionFactor;
    // parallax 效果的 平滑性
    public Single smoothing;

    // parallax是随 Camera移动而产生的
    private Camera cam;
    // camera 移动 产生 parallax，需要记录 其上次的位置
    private Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main;
    }

    // Use this for initialization
    void Start()
    {
        previousCamPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // parallax 是 相机运动自 当前帧与上一帧的 差 再乘以 比例 得到的  相反值
        Single parallax = (previousCamPos.x - cam.transform.position.x) * parallaxScale;

        // 依次移动 连续的 background
        for (int i = 0; i < background.Length; i++)
        {
            Single backgroundTargetPosX = background[i].position.x + parallax * (i * parallaxReductionFactor + 1);

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, background[i].position.y, background[i].position.z);
            background[i].position = Vector3.Lerp(background[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // 设置 Camera 的位置
        previousCamPos = cam.transform.position;
    }
}
