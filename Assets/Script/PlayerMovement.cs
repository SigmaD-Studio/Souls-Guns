using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component of your player sprite

    void Start()
    {
        // Get the SpriteRenderer component from the player GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Move the player
        transform.position += movementDirection * moveSpeed * Time.deltaTime;

        // Determine mouse position relative to player
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 directionToMouse = mousePosition - transform.position;

        // Flip the sprite based on mouse position
        if (directionToMouse.x < 0f)
        {
            spriteRenderer.flipX = true; // Flip horizontally if mouse is left of player
        }
        else
        {
            spriteRenderer.flipX = false; // Do not flip if mouse is right of player
        }
    }

    public static class UtilsClass
    {
        public static Vector3 GetMouseWorldPosition()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0f; // Ensure z position is correct for 2D games
            return worldPosition;
        }
    }
}
