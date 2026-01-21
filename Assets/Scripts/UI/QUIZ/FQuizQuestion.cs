using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FQuizQuestion
{
    [SerializeField] string question;
    [SerializeField] List<FQuizAnswer> answers;

    public FQuizQuestion(string _question, List<FQuizAnswer> _answers)
    {
        question = _question;
        answers = _answers;
    }
}
