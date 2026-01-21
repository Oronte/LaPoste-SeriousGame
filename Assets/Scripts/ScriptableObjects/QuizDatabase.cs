using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Scriptable Objects/Quiz Database")]
public class QuizDatabase : ScriptableObject
{
    public Color ColorA = Color.red;
    public Color ColorB = Color.green;
    public Color ColorC = Color.blue;
    public Color ColorD = Color.yellow;

    public List<QuizQuestion> questions = new List<QuizQuestion>();

    public QuizQuestion AddQuestion()
    {
        int _newID = questions.Count > 0 ? questions[questions.Count - 1].ID + 1 : 0;

        QuizQuestion _question = new QuizQuestion
        {
            ID = _newID,
            QuestionText = "",
            IsMultipleAnswers = false,
        };

        questions.Add(_question);
        return _question;
    }

    public void RecalculateIDS()
    {
        int _count = questions.Count;
        for (int i = 0; i < _count; i++)
        {
            questions[i].ID = i;
        }
    }
}
