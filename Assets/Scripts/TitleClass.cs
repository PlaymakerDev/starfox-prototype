using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleClass : MonoBehaviour
{
    public string sceneName = "GameScene"; // เปลี่ยนชื่อ scene ได้จาก Inspector

    void Start()
    {
        GameObject btnObj = GameObject.Find("StartButton");
        if (btnObj != null)
        {
            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(LoadScene);
            }
            else
            {
                Debug.LogWarning("StartButton ไม่พบ component Button");
            }
        }
        else
        {
            Debug.LogWarning("ไม่พบ GameObject ชื่อ StartButton ใน Hierarchy");
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("sceneName ไม่ได้ถูกตั้งค่า");
        }
    }
}
