using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private Transform aimTransform;


    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 1f;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    void Update()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0,0,angle);
        Debug.Log(angle);
    }

    public static class UtilsClass
    {
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition,Camera.main);
            vec.z = 0f;
            return vec;
        }
        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Camera WorldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, WorldCamera);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera WorldCamera)
        {
            Vector3 worldPosition = WorldCamera.WorldToScreenPoint(screenPosition);
            return worldPosition;
        }
    }
}
