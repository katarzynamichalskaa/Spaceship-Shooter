using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 15f;
    [SerializeField] float maxHorizontalOffset = 1f;

    void Update()
    {
        MoveForward();
        MoveSideways();
    }

    void MoveForward()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = transform.position + new Vector3(0, scrollSpeed, 0);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;
    }
    void MoveSideways()
    {
        float accelX = Input.acceleration.x;
        float horizontalOffset = accelX * maxHorizontalOffset;

        horizontalOffset = Mathf.Clamp(horizontalOffset, -maxHorizontalOffset, maxHorizontalOffset);

        transform.position = new Vector3(horizontalOffset, transform.position.y, transform.position.z);
    }
}
