using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverClass : MonoBehaviour
{
    public string sceneName = "GameScene";
    void Start()
    {
        GameObject btnObj = GameObject.Find("ButtonGamOver");
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
