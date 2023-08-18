using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    HealthManager healthManager;

    float scrollSpeed = 10f;
    float maxHorizontalOffset = 0.25f;
    float horizontalSmoothing = 0.85f;
    private float currentHorizontalOffset;
    private Vector3 targetPosition;
    float timer = 0f;

    void Start()
    {
        healthManager = GameObject.Find("Player").GetComponent<HealthManager>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > 5 && timer < 10)
        {
            scrollSpeed = 12f;
        }

        if (timer > 15 && timer < 25)
        {
            scrollSpeed = 15f;
        }

        if (timer > 30 && timer < 35)
        {
            scrollSpeed = 20f;
        }

        if (HealthManager.ReturnCurrentHealth() <= 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            MoveForward();
            MoveSideways();
        }
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

        currentHorizontalOffset = Mathf.Lerp(currentHorizontalOffset, horizontalOffset, horizontalSmoothing);

        float clampedX = Mathf.Clamp(transform.position.x + currentHorizontalOffset, -1.7f, 1.7f);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
