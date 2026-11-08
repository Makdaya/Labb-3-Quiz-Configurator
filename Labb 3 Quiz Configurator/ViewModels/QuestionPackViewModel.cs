using Labb_3_Quiz_Configurator.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Labb_3_Quiz_Configurator.ViewModels;

internal class QuestionPackViewModel : ViewModelBase
{
    private readonly QuestionPack _model;

    public QuestionPackViewModel(QuestionPack model)
    {
        _model = model;
        Questions = new ObservableCollection<Question>(_model.Questions);
    }

    public string Name
    {
        get => _model.Name;
        set
        {
            if (_model.Name == value)
                return;

            _model.Name = value;
            RaisePropertyChanged();
        }
    }

    public Difficulty Difficulty
    {
        get => _model.Difficulty;
        set
        {
            if (_model.Difficulty == value)
                return;

            _model.Difficulty = value;
            RaisePropertyChanged();
        }
    }

    public int TimeLimitInSeconds
    {
        get => _model.TimeLimitInSeconds;
        set
        {
            if (_model.TimeLimitInSeconds == value)
                return;

            _model.TimeLimitInSeconds = value;
            RaisePropertyChanged();
        }
    }

    public ObservableCollection<Question> Questions { get; }

    public QuestionPack ToModel()
    {
        return new QuestionPack(Name, Difficulty, TimeLimitInSeconds)
        {
            Questions = Questions.ToList()
        };
    }
}
