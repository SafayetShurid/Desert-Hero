using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int score = 0;
    public static ScoreManager instance;
    public Text scoreText;

    [SerializeField]
    private int scoreAmount;
    [SerializeField]
    private int waitAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(IncreaseScore());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitAmount);
            score += scoreAmount;
            scoreText.text = score.ToString();
        }
    }
}

