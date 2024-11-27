using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        ResetButton();
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            button.GetComponent<Image>().color = correctColor;
            quizManager.correct();
        }
        else
        {
            button.GetComponent<Image>().color = wrongColor;
            quizManager.wrong();
        }
    }

    public void ResetButton()
    {
        button.GetComponent<Image>().color = defaultColor;
    }
}
