using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum EAnswerSelect
{
    A = 0, B, C, D
}

public class QuizPanelBehaviour : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] QuizDatabase database = null;
    [SerializeField] GameObject textPrefabs = null;
    [SerializeField] List<TextMeshProUGUI> answersText;

    // Todo remplacer avec les gameobject
    [SerializeField] Button buttonA = null;
    [SerializeField] Button buttonB = null;
    [SerializeField] Button buttonC = null;
    [SerializeField] Button buttonD = null;
    [SerializeField] Button validateAnswer = null;

    [Header("Debug")]
    List<EAnswerSelect> selectedsAnswer = new List<EAnswerSelect>();

    [SerializeField] int currentQuestionId = 0;
    private void OnEnable()
    {
        //To do binding button
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateQuestion(int _questionId)
    {
        if (_questionId < 0 || _questionId >= database.questions.Count)
            return;

    }

    public void SelectAnAnswer(GameObject _trigger, EAnswerSelect _answerCase, bool _isValidation)
    {
        if (!_isValidation)
        {
            selectedsAnswer.Remove(_answerCase);
            return;
        }
        if (!database.questions[currentQuestionId].IsMultipleAnswers)
            selectedsAnswer.Clear();

        selectedsAnswer.Add(_answerCase);
    }

    public void OnValidateAnswer()
    {
        // To do : discussion about the case of 2 bool

        foreach (EAnswerSelect _answerCase in selectedsAnswer)
        {
            //if (database.questions[currentQuestionId].)

            // Je check si les réponse sont bonne,
            // si une réponse n'est pas correspondant
            // alors ces faux.
        }
    }


}
