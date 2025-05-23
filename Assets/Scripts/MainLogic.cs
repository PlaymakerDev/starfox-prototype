using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour
{
    public int GetHP() => Mathf.Max(hp, 0);
    public float GetTimeRemaining() => timer;
    public int GetScore() => score;

    public int maxHP = 5;
    public float countdownTime = 120f;

    private float timer;
    private int score = 0;
    private int hp;

    private bool isPaused = false;
    private GameObject pauseUIInstance;

    void Start()
    {
        hp = maxHP;
        timer = countdownTime;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            if (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
            }
            else
            {
                ShowGameOverUI();
            }
        }
    }

    public void AddScore()
    {
        score += 1;
        Debug.Log($"Score: {score}");
    }

    public void GetDamage()
    {
        hp -= 1;
        Debug.Log($"HP: {hp}");

        if (hp < 0)
        {
            ShowGameOverUI();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;

            GameObject pauseUIPrefab = Resources.Load<GameObject>("UI/Pause");
            if (pauseUIPrefab != null)
            {
                pauseUIInstance = Instantiate(pauseUIPrefab);
            }
            else
            {
                Debug.LogWarning("ไม่พบ Pause UI ใน Resources/UI/Pause");
            }
        }
        else
        {
            Time.timeScale = 1f;

            if (pauseUIInstance != null)
            {
                Destroy(pauseUIInstance);
            }
        }
    }

    private void ShowGameOverUI()
    {
        GameObject goUI = Resources.Load<GameObject>("UI/GameOver");
        if (goUI != null)
        {
            Instantiate(goUI);
        }
        else
        {
            Debug.LogWarning("ไม่พบ GameOver UI ใน Resources/UI/GameOver");
        }

        enabled = false;
    }
}
