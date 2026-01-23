using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizPanelBehaviour : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] QuizDatabase database = null;
    [SerializeField] GameObject textPrefabs = null;
    [SerializeField] TextMeshProUGUI questionText = null;
    [SerializeField] VerticalLayoutGroup layoutMaster = null;

    // Todo remplacer avec les gameobject
    [SerializeField] List<QuizButton>  buttonList;

    [Header("Debug")]
    [SerializeField] List<QuizButton> selectedsAnswer;
    [SerializeField] List<TextMeshProUGUI> answersText;

    [SerializeField] int currentQuestionId = 0;
    [SerializeField] bool isShowingAnswer = false;

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

    private void OnDisable()
    {
        foreach (QuizButton _button in buttonList)
        {
            if (!_button)
                continue;
            _button.OnButtonActivated.RemoveAllListeners();
        }
    }

    void ResetPanel()
    {
        foreach (TextMeshProUGUI _text in answersText)
        {
            Debug.Log($"Clear {_text.text}");
            Destroy(_text.gameObject);
        }
        answersText.Clear();
        selectedsAnswer.Clear();
    }

    public void UpdateQuestion(int _questionId)
    {

        if (!questionText || !database || !textPrefabs)
            return;
        if (_questionId < 0 || _questionId >= database.questions.Count)
        {
            EndGame();
            return;
        }

        ResetPanel();
        QuizQuestion _question = database.questions[_questionId];
        questionText.SetText(_question.QuestionText);
        questionText.color = Color.white;
        currentQuestionId = _questionId;

    
        // attribution des réponse au bouton.
        for (int _i = 0; _i < buttonList.Count; _i++)
        {
            QuizButton _button = buttonList[_i];
            _button.ResetState();
            if (_i >= _question.answers.Count)
            {
                _button.DisableButton(true);
                continue;
            }

            TextMeshProUGUI _text = Instantiate(textPrefabs, layoutMaster.gameObject.transform).GetComponent<TextMeshProUGUI>();
            if (!_text)
                return;
            _text.SetText($"{Convert.ToChar(65 + _i)} - {_question.answers[_i].Text}");
            _text.color = _question.answers[_i].AnswerColor;
            answersText.Add(_text);
            buttonList[_i].SetAnswer(_question.answers[_i]);
        }
    }

    public void SelectAnAnswer(QuizButton _trigger)
    {
        if (selectedsAnswer.Contains(_trigger))
        {
            _trigger.SetButtonColor(Color.white);
            selectedsAnswer.Remove(_trigger);
            return;
        }
        if (!database.questions[currentQuestionId].IsMultipleAnswers)
        {
            foreach(QuizButton _button in selectedsAnswer)
                _button.SetButtonColor(Color.white);
            selectedsAnswer.Clear();
        }
        selectedsAnswer.Add(_trigger);
        _trigger.SetButtonColor(Color.yellow);
    }

    public void OnValidateAnswer()
    {
        if (!questionText || currentQuestionId < 0 || currentQuestionId >= database.questions.Count)
            return;

        if(isShowingAnswer)
        {
            isShowingAnswer = false;
            UpdateQuestion(++currentQuestionId);
            return;
        }

        bool _result = CheckAnswer();
        RevealAnswer();
        isShowingAnswer = true;

        ResetPanel();
        questionText.SetText(_result ? "Bonnes réponses !" : "Mauvaises réponses !");
        questionText.color = _result ? Color.green : Color.red;
    }

    void RevealAnswer()
    {
        foreach (QuizButton _button in buttonList)
        {
            _button.DisableButton(false);
            QuizAnswer _answer = _button.GetAnswer();
            if (_answer == null)
                continue;
            _button.SetButtonColor(_answer.IsCorrect ? Color.green : Color.red);
        }
    }

    bool CheckAnswer()
    {
        foreach (QuizButton _answerCase in selectedsAnswer)
        {
            if (!_answerCase.GetAnswer().IsCorrect)
            {
                return false;
            }
        }
        return true;
    }

    void EndGame()
    {
        ResetPanel();
        questionText.SetText("Fin du QUIZ");
        questionText.color = Color.white;
        foreach (QuizButton _button in buttonList)
        {
            _button.DisableButton(true);
        }
    }
}
