using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video; // Import Video namespace

// Add these lines in your class where other public variables are defined


public class QuizManager : MonoBehaviour
{
    

    public GameObject[] options; // UI buttons for answer options
    public int currentQuestion; // Index of the current question
    public GameObject Quizpanel; // Panel for the quiz
    public GameObject GoPanel; // Panel displayed after game over
    public Text QuestionTxt; // Text field for the question
    public Text ScoreTxt; // Text field for score display
    public TextMeshProUGUI UserNameTxt; // Text field for displaying user name
    public TextMeshProUGUI ScoreDisplayText; // Reference to the UI element to display scores
    public TimerScript timerScript; // Reference to the timer script

    private static List<PlayerScore> playerScores = new List<PlayerScore>(); // List to store player scores
    private int totalQuestions = 0; // Total number of questions
    public int score; // Current score

    // List to store loaded questions
    private List<QuestionsAndAnswers> QnA = new List<QuestionsAndAnswers>();
    private List<int> easyQuestions = new List<int>(); // Indices for easy questions
    private List<int> mediumQuestions = new List<int>(); // Indices for medium questions
    private List<int> hardQuestions = new List<int>(); // Indices for hard questions

    private void Start()
    {
        RunPythonScript();
        LoadQuestions();
        InitializeQuestions();

        totalQuestions = QnA.Count; // Count total questions
        GoPanel.SetActive(false); // Hide game over panel
        GenerateQuestion(); // Start with the first question
        UserNameTxt.text = StartSceneManager.userName; // Set the user name text
    }

    void RunPythonScript()
    {
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.FileName = "python"; // Assuming Python is in the PATH
        startInfo.Arguments = $"{Application.dataPath}/generate_questions.py";
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;

        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Debug.Log(result);
            }
        }
    }

    void LoadQuestions()
    {
        string path = Application.dataPath + "/questions.json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            QnA = JsonConvert.DeserializeObject<List<QuestionsAndAnswers>>(jsonString);
        }
        else
        {
            Debug.LogError("Questions JSON file not found!");
        }
    }

    void InitializeQuestions()
    {
        for (int i = 0; i < QnA.Count; i++)
        {
            if (QnA[i].Difficulty == "Easy") easyQuestions.Add(i);
            else if (QnA[i].Difficulty == "Medium") mediumQuestions.Add(i);
            else hardQuestions.Add(i);
        }
    }

    void GameOver()
    {
        Quizpanel.SetActive(false); // Hide quiz panel
        GoPanel.SetActive(true); // Show game over panel
        ScoreTxt.text = score + "/" + totalQuestions; // Display final score
        RecordScore(StartSceneManager.userName, score); // Record the player's score
        DisplayScores(); // Display all players' scores
    }

    public void retry()
    {
        SceneManager.LoadScene("StartScene"); // Reload the start scene
    }

public void correct() 
{ 
    score++; // Increment score 
    
    Invoke("NextQuestion", 0.5f); // Delay before moving to the next question 
} 

public void wrong() 
{ 
    
    Invoke("NextQuestion", 0.5f); // Delay before moving to the next question 
}


    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            options[i].GetComponent<AnswerScript>().isCorrect = (QnA[currentQuestion].CorrectAnswer == i + 1); // Check correct answer by index
        }
    }

    public void ResetAllButtons() 
    { 
        foreach (var option in options) 
        { 
            option.GetComponent<AnswerScript>().ResetButton(); 
        } 
    }

    void GenerateQuestion()
    {
        if (currentQuestion < totalQuestions)
        {
            QuestionTxt.text = QnA[currentQuestion].Question; // Set the question text
            SetAnswers(); // Set answer options
            timerScript.timeLeft = 45f; // Reset timer for the new question
        }
        else
        {
            GameOver(); // End game if no questions left
        }
    }
    void NextQuestion() 
    { 
        ResetAllButtons(); 
        GenerateNextQuestion(timerScript.timeLeft); // Generate the next question based on time left
    }
    void GenerateNextQuestion(float timeLeft)
    {
        int nextQuestionIndex = -1;

        // Determine which question to show next based on time left
        if (timeLeft > 10f)
        {
            if (hardQuestions.Count > 0)
            {
                nextQuestionIndex = hardQuestions[0]; // Take from hard questions
                hardQuestions.RemoveAt(0); // Remove it from the list
            }
            else if (mediumQuestions.Count > 0)
            {
                nextQuestionIndex = mediumQuestions[0]; // Take from medium questions
                mediumQuestions.RemoveAt(0);
            }
            else if (easyQuestions.Count > 0)
            {
                nextQuestionIndex = easyQuestions[0]; // Take from easy questions
                easyQuestions.RemoveAt(0);
            }
        }
        else if (timeLeft > 5f)
        {
            if (mediumQuestions.Count > 0)
            {
                nextQuestionIndex = mediumQuestions[0]; // Take from medium questions
                mediumQuestions.RemoveAt(0);
            }
            else if (hardQuestions.Count > 0)
            {
                nextQuestionIndex = hardQuestions[0]; // If medium exhausted, take hard if available
                hardQuestions.RemoveAt(0);
            }
            else if (easyQuestions.Count > 0)
            {
                nextQuestionIndex = easyQuestions[0]; // Take from easy questions
                easyQuestions.RemoveAt(0);
            }
        }
        else
        {
            if (easyQuestions.Count > 0)
            {
                nextQuestionIndex = easyQuestions[0]; // Take from easy questions
                easyQuestions.RemoveAt(0);
            }
            else if (mediumQuestions.Count > 0)
            {
                nextQuestionIndex = mediumQuestions[0]; // If easy exhausted, take medium if available
                mediumQuestions.RemoveAt(0);
            }
            else if (hardQuestions.Count > 0)
            {
                nextQuestionIndex = hardQuestions[0]; // If easy and medium exhausted, take hard if available
                hardQuestions.RemoveAt(0);
            }
        }

        // Set the current question based on the selected index
        if (nextQuestionIndex != -1)
        {
            currentQuestion = nextQuestionIndex; // Update the current question
            GenerateQuestion(); // Call to display the new question
        }
        else
        {
            GameOver(); // End the game if no more questions are available
        }
    }

    void RecordScore(string playerName, int score)
    {
        playerScores.Add(new PlayerScore(playerName, score)); // Add player's score to the list
    }


    void DisplayScores()
    {
        // Clear previous scores
      //  ScoreDisplayText.text = ""; 

        // Sort the playerScores list in descending order based on scores
        playerScores.Sort((x, y) => y.score.CompareTo(x.score));

        // Display each player's score
        foreach (var playerScore in playerScores)
        {
            ScoreDisplayText.text += playerScore.playerName + ": " + playerScore.score + "\n";
        }
    }
}
