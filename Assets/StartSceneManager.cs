using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public static string userName;

    public void StartQuiz()
    {
        userName = nameInputField.text;
        SceneManager.LoadScene("CGV UI");
    }
}
