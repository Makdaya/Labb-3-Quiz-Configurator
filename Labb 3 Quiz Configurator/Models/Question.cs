using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_3_Quiz_Configurator.Models;

class Question
{
    public Question(string query, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
    {
        Query = query;
        CorrectAnswer = correctAnswer;
        IncorrectAnswers = new string[] { incorrectAnswer1, incorrectAnswer2, incorrectAnswer3 };
    }

    public string Query { get; set; }
    public string CorrectAnswer { get; set; }
    public string[] IncorrectAnswers { get; set; }
}