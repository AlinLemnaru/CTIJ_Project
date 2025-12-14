using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 6f;
    private float destroyX = -30f;   // far left off-screen

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
}
