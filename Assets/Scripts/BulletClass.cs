using UnityEngine;

public class BulletClass : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 1.5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
