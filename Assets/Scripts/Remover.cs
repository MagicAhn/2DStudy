using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
    public GameObject splash;

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
        // 如果是 Player 掉了下来
        if (other.gameObject.tag.Equals("Player"))
        {
            // 相机不再追随 Player
            //GameObject.FindGameObjectWithTag("mainCamera").GetComponent<CameraFollow>().enabled = false;
            // 血条不复存在
            if (GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
            {
                GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
            }

            // 溅起了水花
            Instantiate(splash, other.gameObject.transform.position, transform.rotation);
            // 销毁 Player
            Destroy(other.gameObject);
            // 重新加载游戏
            StartCoroutine(ReloadGame());
        }
        else
        {
            Instantiate(splash, collider.transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(Application.loadedLevel);
    }
}
