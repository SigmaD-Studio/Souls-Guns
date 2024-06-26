using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("References")]
    public Transform aimTransform; // Assign the Aim object in the Inspector

    [Header("Attributes")]
    [SerializeField] public float aimDistance = 1f; // Distance from player to aimTransform

    void Update()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        // Calculate direction from player to mouse position
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Calculate angle in radians
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x);

        // Convert angle to degrees and clamp to avoid flipping upside down
        float angleDegrees = Mathf.Clamp(angle * Mathf.Rad2Deg, angle, angle);

        // Rotate the aimTransform to face the mouse position horizontally
        aimTransform.eulerAngles = new Vector3(0, 0, angleDegrees);

        // Position the aimTransform at a fixed distance from the player, freezing y position
        Vector3 targetPosition = transform.position + aimDirection * aimDistance;
        aimTransform.position = new Vector3(targetPosition.x, aimTransform.position.y, aimTransform.position.z);
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
