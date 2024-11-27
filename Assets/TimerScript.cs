using System.Collections;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float timeLeft = 45f; // Time for each question
    public TMP_Text timerText; 
    public QuizManager quizManager; // Reference to the QuizManager

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }

        quizManager.wrong();
        StartCoroutine(StartTimer());
    }

    void UpdateTimerText()
    {
        timerText.text = Mathf.Round(timeLeft).ToString();
    }
}
