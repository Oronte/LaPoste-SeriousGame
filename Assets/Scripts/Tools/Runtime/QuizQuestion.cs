using System;
using System.Collections.Generic;

[Serializable]
public class QuizQuestion
{
    private int id;

    public string QuestionText;
    public bool IsMultipleAnswers = false;
    public List<QuizAnswer> answers = new List<QuizAnswer>();

    public int ID => id;

    public void SetID(int _newID)
    {
        id = _newID;
    }
}
