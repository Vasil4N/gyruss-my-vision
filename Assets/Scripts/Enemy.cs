using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private float speedVelocity = 1.5f;
    [SerializeField]
    private float speedExpandRadius = 0.05f;
    [SerializeField]
    private float timeScale = 2f;

    private float radiusVariationX = 1;
    private float radiusVariationY = 1;
    private float direction = 1;

    private float speedExpandScale;
    private float moveTime;
    private float radius = 0.1f;
    private readonly float maxRadious = 3.5f;
    private readonly float maxScale = 1.0f;

    private void Start()
    {
        speedExpandScale = speedExpandRadius / 10;
    }

    public void Initialize(float radiusX, float radiusY, float dir, Sprite sprite)
    {
        radiusVariationX = radiusX;
        radiusVariationY = radiusY;
        direction = dir;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void FixedUpdate()
    {
        MoveEnemy();
        Expand();
        moveTime += timeScale * Time.deltaTime;
    }

    private void MoveEnemy()
    {
        float angle = speedVelocity * moveTime;
        Vector2 position;

        if (direction > 1f)
        {
            position = new Vector2(Mathf.Cos(angle) * radius * radiusVariationX, Mathf.Sin(angle) * radius * radiusVariationY);
        }
        else
        {
            position = new Vector2(Mathf.Sin(angle) * radius * radiusVariationX, Mathf.Cos(angle) * radius * radiusVariationY);
        }

        transform.position = position;
    }

    private void Expand()
    {
        if (radius < maxRadious)
        {
            radius += speedExpandRadius;
        }
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(speedExpandScale, speedExpandScale, speedExpandScale);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            GameObject.Find("Levels").GetComponent<Levels>().enemyKilled(maxRadious - radius);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
