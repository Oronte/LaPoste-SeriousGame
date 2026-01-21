using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Scriptable Objects/Quiz Database")]
public class QuizDatabase : ScriptableObject
{
    public List<QuizQuestion> questions = new List<QuizQuestion>();

    public void AddQuestion()
    {
        QuizQuestion _question = new QuizQuestion();
        _question.SetID(questions.Count);
        questions.Add(_question);
    }

    public void RecalculateIDS()
    {
        int _count = questions.Count;
        for (int i = 0; i < _count; i++)
        {
            questions[i].SetID(i);
        }
    }
}
