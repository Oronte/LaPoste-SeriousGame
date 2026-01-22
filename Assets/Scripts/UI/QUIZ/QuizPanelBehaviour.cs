using System;
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
    [SerializeField] TextMeshProUGUI questionText = null;
    [SerializeField] VerticalLayoutGroup layoutMaster = null;

    // Todo remplacer avec les gameobject
    [SerializeField] List<QuizButton>  buttonList;
    [SerializeField] Button validateAnswer = null;

    [Header("Debug")]
    [SerializeField] List<QuizButton> selectedsAnswer;
    [SerializeField] List<TextMeshProUGUI> answersText;

    [SerializeField] int currentQuestionId = 0;
    private void OnEnable()
    {
        UpdateQuestion(0);

        foreach (QuizButton _button in buttonList)
        {
            if (!_button)
                continue;
            _button.OnButtonActivated.AddListener(SelectAnAnswer);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDisable()
    {
        foreach (QuizButton _button in buttonList)
        {
            if (!_button)
                continue;
            _button.OnButtonActivated.RemoveAllListeners();
        }
    }

    public void UpdateQuestion(int _questionId)
    {

        if (!questionText || !database || !textPrefabs)
            return;
        if (_questionId < 0 || _questionId >= database.questions.Count)
            return;

        foreach (TextMeshProUGUI _text in answersText)
        {
            Destroy(_text);
        }

        QuizQuestion _question = database.questions[_questionId];
        questionText.SetText(_question.QuestionText);
        currentQuestionId = _questionId;

        selectedsAnswer.Clear();
        // attribution des réponse au bouton.
        for (int _i = 0; _i < _question.answers.Count; _i++)
        {
            TextMeshProUGUI _text = Instantiate(textPrefabs, layoutMaster.gameObject.transform).GetComponent<TextMeshProUGUI>();
            if (!_text)
                return;
            _text.SetText($"{Convert.ToChar(65 + _i)} - {_question.answers[_i].Text}");
            _text.color = _question.answers[_i].AnswerColor;
            answersText.Add(_text);

            buttonList[_i].SetAnswer(_question.answers[_i]);
            buttonList[_i].ResetState();
        }
    }

    public void SelectAnAnswer(QuizButton _trigger)
    {
        if (!selectedsAnswer.Contains(_trigger))
        {
            selectedsAnswer.Remove(_trigger);
            return;
        }
        if (!database.questions[currentQuestionId].IsMultipleAnswers)
        {
            foreach(QuizButton _button in selectedsAnswer)
                _button.SetButtonColor(Color.red);
            selectedsAnswer.Clear();
        }

        selectedsAnswer.Add(_trigger);
        _trigger.SetButtonColor(Color.green);
    }

    public void OnValidateAnswer()
    {

        foreach (QuizButton _answerCase in selectedsAnswer)
        {
            if (!_answerCase.GetAnswer().IsCorrect)
            {
                return;
            }
        }

        // Tous est Good!
    }


}
