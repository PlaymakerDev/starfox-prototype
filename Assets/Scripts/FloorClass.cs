using UnityEngine;

public class FloorClass : MonoBehaviour
{
    public float speed = 0.5f;
    void Update()
    {
        float offset = Time.time * speed;                             
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, -offset);
    }
}
