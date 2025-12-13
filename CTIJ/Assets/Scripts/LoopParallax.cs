using UnityEngine;

public class LoopParallax : MonoBehaviour
{
    [SerializeField] private float speed;        // how fast this layer moves left
    [SerializeField] private float startDelay = 2.15f;   // delay before it starts
    [SerializeField] private bool endless = true;     // disable if you only want one pass

    Transform tileA;
    Transform tileB;
    Transform tileC;
    float tileWidth;

    float timer;
    bool started;

    void Start()
    {
        if (transform.childCount < 3)
        {
            Debug.LogError(name + " needs 3 children (tiles A,B,C).");
            enabled = false;
            return;
        }

        tileA = transform.GetChild(0);
        tileB = transform.GetChild(1);
        tileC = transform.GetChild(2);

        SpriteRenderer sr = tileA.GetComponentInChildren<SpriteRenderer>();
        tileWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // delay
        if (!started)
        {
            timer += Time.deltaTime;
            if (timer < startDelay) return;
            started = true;
        }

        // move the whole layer
        Vector3 move = Vector3.left * speed * Time.deltaTime;
        transform.position += move;

        if (!endless) return;

        // recycle any tile that went off the left side
        RecycleIfOffscreen(tileA);
        RecycleIfOffscreen(tileB);
        RecycleIfOffscreen(tileC);
    }

    void RecycleIfOffscreen(Transform tile)
    {
        // world x of the tile's right edge
        float rightEdge = tile.position.x + tileWidth * 0.5f;

        // world x of the camera's left edge
        float camLeft = Camera.main.transform.position.x -
                        Camera.main.orthographicSize * Camera.main.aspect;

        // only recycle when the entire tile is left of the screen
        if (rightEdge < camLeft)
        {
            // find current rightmost tile
            Transform rightmost = tileA;
            if (tileB.position.x > rightmost.position.x) rightmost = tileB;
            if (tileC.position.x > rightmost.position.x) rightmost = tileC;

            // move this tile to the right of the rightmost one
            tile.position = new Vector3(
                rightmost.position.x + tileWidth,
                tile.position.y,
                tile.position.z
            );
        }
    }

}
