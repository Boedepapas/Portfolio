using UnityEngine;

public class orbit : MonoBehaviour
{
    public Transform centerPoint;
    public float radius;
    public float orbitSpeed;
    
    public int childNum;
    private int childCount;
    private float angleOffset;
    private float anglePerChild;
    private float angle;
    private float rotationAngle;

    void Start()
    {   
        rotationAngle = transform.rotation.z;
        childCount = transform.parent.childCount;
        anglePerChild = 360f / childCount;
        angleOffset = anglePerChild * childNum * Mathf.Deg2Rad;
    }

    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;
        float x = centerPoint.position.x + Mathf.Cos(angle + angleOffset) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle + angleOffset) * radius;
        transform.position = new Vector2(x, y);
        
        Vector2 direction = (transform.position - centerPoint.position).normalized;
        rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle - 90f);   
    }
}


