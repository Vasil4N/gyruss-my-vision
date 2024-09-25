using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    Sprite[] stages;

    private int pos = 0;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeStage());
    }

    IEnumerator ChangeStage()
    {
        while (pos < stages.Length)
        {
            spriteRenderer.sprite = stages[pos];
            pos++;
            yield return new WaitForSeconds(0.12f);
        }

        Destroy(gameObject);
    }
}
