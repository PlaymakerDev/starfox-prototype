using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public float moveSpeed = 7f;
    public List<KeyCode> fireKeys = new List<KeyCode> { KeyCode.P };

    private GameObject bulletPrefab;

    void Start()
    {
        // โหลด Bullet Prefab จาก Resources/Prefabs/Bullets
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        if (bulletPrefab == null)
        {
            Debug.LogWarning("ไม่พบ Bullet Prefab ที่ Resources/Prefabs/Bullet");
        }
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 delta = new Vector3(moveX, moveY, 0f) * moveSpeed * Time.deltaTime;

        transform.position += delta;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }

    void Shoot()
    {
        foreach (KeyCode key in fireKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (bulletPrefab != null)
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindObjectOfType<MainLogic>()?.GetDamage();
        }
    }
}
 