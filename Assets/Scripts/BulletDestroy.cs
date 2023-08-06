using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    [SerializeField] float destroyDelay = 1.5f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
