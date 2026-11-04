using System;
using System.Collections.Generic;
using System.Text;
using Labb_3_Quiz_Configurator.Command;
using Labb_3_Quiz_Configurator.Models;

namespace Labb_3_Quiz_Configurator.ViewModels;

internal class ConfigurationViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;
    private Question? _selectedQuestion;

    public Question? SelectedQuestion
    {
        get => _selectedQuestion;
        set => SetProperty(ref _selectedQuestion, value);
    }

    public RelayCommand ChangePackNameCommand { get; }
    public RelayCommand AddQuestionCommand { get; }
    public RelayCommand RemoveQuestionCommand { get; }

    private string _demoText = string.Empty;
    public string DemoText {
        get => _demoText;
        set
        {
            if (SetProperty(ref _demoText, value))
            {
                ChangePackNameCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }

    public void RaiseActivePackChanged() => RaisePropertyChanged(nameof(ActivePack));

    public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;

        ChangePackNameCommand = new RelayCommand(_ => ChangePackName(), _ => DemoText != string.Empty);

        AddQuestionCommand = new RelayCommand(_ => AddQuestion());
        RemoveQuestionCommand = new RelayCommand(_ => RemoveQuestion());
    }

    private bool CanChangePackName(object? arg)
    {
        return DemoText != string.Empty;
    }

    private void ChangePackName()
    {
        if (ActivePack != null)
        {
            ActivePack.Name = DemoText;
        }
    }
    private void AddQuestion()
    {
        if (ActivePack != null)
        {
            var question = new Question(
                "New question",
                "Correct answer",
                "Incorrect answer 1",
                "Incorrect answer 2",
                "Incorrect answer 3");

            ActivePack.Questions.Add(question);
            SelectedQuestion = question;
            
        }
    }
    private void RemoveQuestion()
    {
        if (ActivePack != null && SelectedQuestion != null)
        {
            ActivePack.Questions.Remove(SelectedQuestion);
            SelectedQuestion = null;
        }
    }
}