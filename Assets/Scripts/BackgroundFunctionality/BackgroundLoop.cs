using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] float choke = 0.1f;
    [SerializeField] GameObject[] backgrounds;
    private Camera mainCamera;
    private Vector2 screenBounds;
    float scrollSpeed = 10f;

    private bool isFading = false;


    float timer = 0f;

    void Start()
    {
        timer = 0f;

        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        foreach (GameObject obj in backgrounds)
        {
            loadChildObjects(obj);
        }
    }

    void loadChildObjects(GameObject obj)
    {
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);

        if (obj.transform.childCount == 0)
        {
            GameObject clone = Instantiate(obj) as GameObject;
            for (int i = 0; i <= childsNeeded; i++)
            {
                GameObject c = Instantiate(clone) as GameObject;
                c.transform.SetParent(obj.transform);
                c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
                c.name = obj.name + i;
            }

            Destroy(clone);
            Destroy(obj.GetComponent<SpriteRenderer>());
        }
    }

    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;

            if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjectHeight)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);
            }
            else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjectHeight)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.x - halfObjectHeight * 2, firstChild.transform.position.z);
            }
        }
    }

    void Update()
    {
        MoveForward();
    }

    void LateUpdate()
    {
        timer += Time.deltaTime;

        if(timer > 0) 
        {
            foreach (GameObject obj in backgrounds)
            {
                repositionChildObjects(obj);
            }
        }

        if (timer > 5 && timer < 10)
        {
            scrollSpeed = 12f;
            Fade(0);
        }

        if(timer > 15 && timer < 25 )
        {
            scrollSpeed = 15f;
            Fade(1);
        }

        if (timer > 30 && timer < 35)
        {
            scrollSpeed = 20f;
            Fade(2);
        }
    }

    public void MoveForward()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = transform.position + new Vector3(0, scrollSpeed, 0);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;
    }

    private IEnumerator FadeBackgroundOut(SpriteRenderer spriteRenderer, float duration)
    {
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Color newColor = Color.Lerp(startColor, endColor, elapsedTime / duration);
            spriteRenderer.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = endColor;

        isFading = false; 
    }

    void Fade(int index)
    {
        if (!isFading)
        {
            isFading = true;
            foreach (SpriteRenderer spriteRenderer in backgrounds[index].GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(FadeBackgroundOut(spriteRenderer, 5f));
            }
        }
    }
}
