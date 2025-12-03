using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // Assign your Player in Inspector
    public Collider2D levelBounds;    // Assign the LevelBounds BoxCollider2D
    public float smoothSpeed = 0.125f;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;
    }

    void LateUpdate()
    {
        Vector3 targetPos = player.position;

        // Get bounds from the collider
        Bounds bounds = levelBounds.bounds;

        // Clamp camera position inside collider bounds
        float clampedX = Mathf.Clamp(targetPos.x, bounds.min.x + camHalfWidth, bounds.max.x - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPos.y, bounds.min.y + camHalfHeight, bounds.max.y - camHalfHeight);

        Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, transform.position.z), smoothSpeed);
        transform.position = smoothPos;
    }
}