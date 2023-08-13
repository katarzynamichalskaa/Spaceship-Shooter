using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreText;         
    [SerializeField] float scoreIncreaseRate = 1.0f;

    private int score = 0; 
    private float timeElapsed = 0.0f; 
    private bool isGameOver = false;

    private void Start()
    {
        UpdateScoreText();

        InvokeRepeating("IncreaseScoreOverTime", 1.0f, 1.0f);
    }

    private void Update()
    {
        if (isGameOver)
        {
            //here new scene/canvas + converting score to money
        }
    }

    private void IncreaseScoreOverTime()
    {
        if (!isGameOver)
        {
            timeElapsed += 1.0f;
            score += Mathf.RoundToInt(scoreIncreaseRate * timeElapsed);
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text =  score.ToString();
    }

}
