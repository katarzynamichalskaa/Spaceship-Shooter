using UnityEngine;

public class DestroyIt : MonoBehaviour
{
    [SerializeField] float destroyDelay = 1.5f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
