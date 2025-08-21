using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;


    private void Start()
    {
        // 规过 1
        var spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit;

        // 规过 2
        //var collider = GetComponent<BoxCollider2D>();
        //width = collider.size.x;
    }

    private void Update()
    {
        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2f, 0f, 0f);
        }
    }
}