using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject Center;
    [SerializeField]
    private GameObject Shot;
    [SerializeField]
    private GameObject ShotGunPoint;
    [SerializeField]
    private GameObject ShotsContainer;
    [SerializeField]
    private float speed = 1;

    private int timeBetweenShots = 0;
    private readonly int shotCooldown = 10;

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (Input.GetButton("Fire1") && timeBetweenShots > shotCooldown)
        {
            Shoot();
            timeBetweenShots = 0;
        }
        timeBetweenShots++;

        if (Mathf.Abs(h) > 0.5)
        {
            Center.transform.Rotate(new Vector3(0, 0, Mathf.Sign(h) * speed), Space.Self);
        }

    }

    void Shoot()
    {
        Instantiate(Shot, ShotGunPoint.transform.position, ShotGunPoint.transform.rotation, ShotsContainer.transform);
        gameObject.GetComponent<AudioSource>().Play();
    }
}
