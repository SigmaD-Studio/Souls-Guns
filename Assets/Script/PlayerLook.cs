using UnityEngine;

public class PlayerFlipSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        FlipSprite();
    }

    void FlipSprite()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerToMouseDir = mousePos - transform.position;

        // Flip the sprite based on the mouse position
        if (playerToMouseDir.x != 0)
        {
            spriteRenderer.flipX = playerToMouseDir.x < 0;
        }
    }
}
