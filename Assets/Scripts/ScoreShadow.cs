using UnityEngine;
using System.Collections;

public class ScoreShadow : MonoBehaviour
{
    public GameObject guiCopy;

    void Awake()
    {
        // 让影子 在 Score下面一点点
        Vector3 behindPos = transform.position;
        behindPos = new Vector3(guiCopy.transform.position.x, guiCopy.transform.position.y - 0.1f, guiCopy.transform.position.z - 1);
        transform.position = behindPos;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 影子的 text 和 正身的 text相同
        guiText.text = guiCopy.guiText.text;
    }
}
