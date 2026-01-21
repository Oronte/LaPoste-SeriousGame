using System;
using UnityEngine;

[Serializable]
public struct FQuizAnswer
{
    public string answerString;
    public bool isAnAnswer;

    public FQuizAnswer(string _answerString, bool _isAnAnswer)
    {
        answerString = _answerString;
        isAnAnswer = _isAnAnswer;
    }
}
