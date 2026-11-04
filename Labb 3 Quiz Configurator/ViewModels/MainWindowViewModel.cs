using Labb_3_Quiz_Configurator.Models;
using Labb_3_Quiz_Configurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Labb_3_Quiz_Configurator.Command;

namespace Labb_3_Quiz_Configurator.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<QuestionPackViewModel> Packs { get; }
    public RelayCommand AddPackCommand { get; }

    private QuestionPackViewModel _activePack;
    public QuestionPackViewModel ActivePack
    {
        get => _activePack;
        set
        {
            if (SetProperty(ref _activePack, value))
            {
                ConfigurationViewModel?.RaiseActivePackChanged();
            }
        }
    }

    public PlayerViewModel? PlayerViewModel { get; }
    public ConfigurationViewModel? ConfigurationViewModel { get; }


    public MainWindowViewModel()
    {
        PlayerViewModel = new PlayerViewModel(this);
        ConfigurationViewModel = new ConfigurationViewModel(this);
        AddPackCommand = new RelayCommand(_ => AddPack());

        var questionPack = new QuestionPack("Mina frågor!", Difficulty.Easy);
        questionPack.Questions.Add(new Question("Vad heter sveriges huvudstad?", "Stockholm", "Oslo", "Göteborg", "London"));
        questionPack.Questions.Add(new Question("Vad är 2 + 3?", "5", "10", "8", "1"));

        var packViewModel = new QuestionPackViewModel(questionPack);

        Packs = new ObservableCollection<QuestionPackViewModel>
{
    packViewModel
};
        ActivePack = packViewModel;
    }

    private void AddPack()
    {
        var questionPack = new QuestionPack("New question pack", Difficulty.Easy);

        var packViewModel = new QuestionPackViewModel(questionPack);

        Packs.Add(packViewModel);

        ActivePack = packViewModel;
    }
}
