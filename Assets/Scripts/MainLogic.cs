using System.Collections;
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

    private bool isInvincible = false;
    private float playerInvincibilityDuration = 0.3f;

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
        // ดักถ้า player โดนชน ไม่ให้รับดาเม็จซ้อน
        if (isInvincible) return;

        hp -= 1;
        Debug.Log($"HP: {hp}");

        // ถ้า HP น้อยกว่าหรือเท่ากับ 0 = ต า ย
        if (hp <= 0)
        {
            ShowGameOverUI();
        }

        // ทำงานหลังมีการคำนวน damage ครั้งแรก
        StartCoroutine(PlayerInvincibility());
    }

    // delay การรับดาเม็จ
    private IEnumerator PlayerInvincibility()
    {
        isInvincible = true;
        // หลังโดนดาเม็จให้ delay 0.3 วินาที
        yield return new WaitForSeconds(playerInvincibilityDuration);
        isInvincible = false;
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
        // ไม่ให้เกมทำงานต่อหลังจากที่ขึ้น Game Over UI
        Time.timeScale = 0f;
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
