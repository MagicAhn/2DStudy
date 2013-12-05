using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// 设置 particle system 的 sort layer
/// </summary>
public class SetParticleSortingLayer : MonoBehaviour
{
    public String sortingLayerName;

    // Use this for initialization
    void Start()
    {
        particleSystem.renderer.sortingLayerName = sortingLayerName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
