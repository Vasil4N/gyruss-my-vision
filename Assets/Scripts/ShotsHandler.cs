using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float shotSpeed = 1;

    private readonly float centerDestroyDistance = 0.3f;

    void FixedUpdate()
    {
        GameObject child;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            child = gameObject.transform.GetChild(i).gameObject;
            child.transform.position = Vector2.MoveTowards(child.transform.position, Vector2.zero, shotSpeed);
            if (child.transform.position.magnitude < centerDestroyDistance)
            {
                Destroy(child);
            }
        }
    }
}
