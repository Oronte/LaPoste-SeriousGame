using System;

[Serializable]
public class QuizQuestion
{
    public int ID;
    public string QuestionText;

    public bool IsMultipleAnswers;

    public QuizAnswer[] Answers = new QuizAnswer[4]
    {
        new QuizAnswer(),
        new QuizAnswer(),
        new QuizAnswer(),
        new QuizAnswer()
    };

    public bool[] CorrectAnswers = new bool[4];
}
