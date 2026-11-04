using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Labb_3_Quiz_Configurator.Models;

class Question : INotifyPropertyChanged
{
    public Question(string query, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
    {
        Query = query;
        CorrectAnswer = correctAnswer;
        IncorrectAnswers = new string[] { incorrectAnswer1, incorrectAnswer2, incorrectAnswer3 };
    }

    private string _query;

    public string Query
    {
        get => _query;
        set
        {
            if (_query != value)
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }
    }
    public string CorrectAnswer { get; set; }
    public string[] IncorrectAnswers { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
