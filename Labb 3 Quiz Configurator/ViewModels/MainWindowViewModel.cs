using Labb_3_Quiz_Configurator.Command;
using Labb_3_Quiz_Configurator.Models;
using Labb_3_Quiz_Configurator.Services;
using Labb_3_Quiz_Configurator.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Labb_3_Quiz_Configurator.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<QuestionPackViewModel> Packs { get; }
    public RelayCommand AddPackCommand { get; }
    public RelayCommand DeletePackCommand { get; }
    public RelayCommand PlayModeCommand { get; }
    public RelayCommand EditModeCommand { get; }
    public RelayCommand SaveCommand { get; }
    public RelayCommand LoadCommand { get; }
    private readonly Jsonservices _jsonService = new Jsonservices();

    private bool _isPlayMode;

    public bool IsPlayMode
    {
        get => _isPlayMode;
        set => SetProperty(ref _isPlayMode, value);
    }


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
        DeletePackCommand = new RelayCommand(_ => DeletePack());
        PlayModeCommand = new RelayCommand(_ => IsPlayMode = true);
        EditModeCommand = new RelayCommand(_ => IsPlayMode = false);
        SaveCommand = new RelayCommand(async _ => await SavePacksAsync());
        LoadCommand = new RelayCommand(async _ => await LoadPacksAsync());

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

    private void DeletePack()
    {
        if (Packs.Count > 1 && ActivePack != null)
        {
            Packs.Remove(ActivePack);
            ActivePack = Packs[0];
        }
    }

    private async Task SavePacksAsync()
    {
        SaveFileDialog dialog = new SaveFileDialog
        {
            Filter = "JSON files (*.json)|*.json",
            DefaultExt = ".json",
            AddExtension = true
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                List<QuestionPack> packs = Packs
                    .Select(pack => pack.ToModel())
                    .ToList();

                await _jsonService.SaveAsync(packs, dialog.FileName);

                MessageBox.Show("Frågepaketen har sparats!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Det gick inte att spara: {ex.Message}");
            }
        }
    }

    private async Task LoadPacksAsync()
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json"
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                List<QuestionPack> packs =
                    await _jsonService.LoadAsync(dialog.FileName);

                Packs.Clear();

                foreach (QuestionPack pack in packs)
                {
                    Packs.Add(new QuestionPackViewModel(pack));
                }

                ActivePack = Packs.FirstOrDefault();

                MessageBox.Show("Frågepaketen har lästs in!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Det gick inte att läsa in filen: {ex.Message}");
            }
        }
    }
}
