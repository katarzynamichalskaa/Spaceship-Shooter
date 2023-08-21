using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    HealthManager healthManager;

    float scrollSpeed = 12f;
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

        //scroll speed
        if (timer > 20f && timer < 30f)
        {
            scrollSpeed = 15f;
        }

        else if (timer > 30f && timer < 60f)
        {
            scrollSpeed = 18f;
        }

        else if (timer > 60 && timer < 120f)
        {
            scrollSpeed = 23f;
        }
        else if (timer > 120f)
        {
            scrollSpeed = 26f;
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
