using System;
using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    // 这个 gameobject 是否应该在 Awake后的一段延时后 被消除 
    public Boolean destroyAwake;
    public Single awakeDestroyDelay;
    // 是否找到其 child gameobject 然后删除
    public Boolean findChild;
    public String nameChild;

    void Awake()
    {
        if (destroyAwake)
        {
            if (findChild)
            {
                Destroy(transform.Find(nameChild).gameObject);
            }
            else
            {
                Destroy(transform.gameObject, awakeDestroyDelay);
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroyChildGameObject()
    {
        if (gameObject.transform.Find(nameChild).gameObject != null)
        {
            Destroy(transform.Find(nameChild).gameObject);
        }
    }

    void DisableChildGameObject()
    {
        if (transform.Find(nameChild).gameObject.activeSelf)
        {
            transform.Find(nameChild).gameObject.SetActive(false);
        }
    }

    void DestroyGameObejct()
    {
        Destroy(gameObject);
    }
}
