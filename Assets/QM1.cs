// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI; // Add this line
// using UnityEngine.SceneManagement;

// public class QuizManager : MonoBehaviour
// {
//     public List<QuestionsAndAnswers> QnA;
//     public GameObject[] options;
//     public int currentQuestion; // Corrected variable name

//     public GameObject Quizpanel;
//     public GameObject GoPanel;

//     public UnityEngine.UI.Text QuestionTxt; 

//     public Text ScoreTxt;
//     int totalQuestions = 0;
//     public int score;


//     private void Start()
//     {   
//         totalQuestions = QnA.Count;
//         GoPanel.SetActive(false);
//         generateQuestion(); // Corrected function name
//     }

//     void GameOver(){
//         Quizpanel.SetActive(false);
//         GoPanel.SetActive(true);
//         ScoreTxt.text = score + "/" + totalQuestions;
//     }

//     public void retry()
//         {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//         }

//     public void correct()
//         {
//         score= score +1;
//         QnA.RemoveAt(currentQuestion);
//         generateQuestion();
//         }

//         public void wrong()
//         {
//         QnA.RemoveAt(currentQuestion);
//         generateQuestion();
//         }

//     void SetAnswers()
//     {
//         for (int i = 0; i < options.Length; i++)
//         {
//             options[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = QnA[currentQuestion].Answers[i];
        
//         if (QnA[currentQuestion].CorrectAnswer == i+1)
//         {
//         options[i].GetComponent<AnswerScript>().isCorrect = true;
//         }
        
//         }
//     }

//         void generateQuestion()
// {
//     // if(QnA == null || QnA.Count == 0)
//     // {
//     //     Debug.LogError("QnA list is empty or null!");
//     //     return;
//     // }

//     if (QnA.Count > 0) {
//    currentQuestion = Random.Range(0, QnA.Count);

//     QuestionTxt.text = QnA[currentQuestion].Question;
//     SetAnswers();
// }
//     else
//             {
//         Debug.Log("Out of Questions");
//         GameOver();
//         }

//         }



//     // public DynamicLightingController lightingController;  // Reference to the lighting controller script

//     // // Simulated function to handle quiz answers
//     // public void OnAnswerSelected(bool isCorrect)
//     // {
//     //     if (isCorrect)
//     //     {
//     //         // Call the lighting function for a correct answer
//     //         lightingController.HighlightOnCorrectAnswer();
//     //     }
//     //     else
//     //     {
//     //         // Call the lighting function for an incorrect answer
//     //         lightingController.HighlightOnError();
//     //     }

//     //     // Reset the lighting after a delay
//     //     Invoke("ResetLighting", 2.0f);
//     // }

//     // // Reset the lighting back to default
//     // void ResetLighting()
//     // {
//     //     lightingController.ResetLighting();
//     // }

// }
