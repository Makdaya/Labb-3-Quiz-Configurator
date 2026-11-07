using Labb_3_Quiz_Configurator.Models;
using Labb_3_Quiz_Configurator.Command;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Labb_3_Quiz_Configurator.ViewModels;

internal class PlayerViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;
    private DispatcherTimer? _timer;

    public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;

        StartQuizCommand = new RelayCommand(_ => StartQuiz());
        CheckAnswerCommand = new RelayCommand(parameter => CheckAnswer(parameter?.ToString()));
        NextQuestionCommand = new RelayCommand(_ => NextQuestion());
      
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _timer.Tick += Timer_Tick;
    }

    public RelayCommand StartQuizCommand { get; }
    public RelayCommand CheckAnswerCommand { get; }
    public RelayCommand NextQuestionCommand { get; }

    private ObservableCollection<Question> _quizQuestions = new();

    public ObservableCollection<Question> QuizQuestions
    {
        get => _quizQuestions;
        set => SetProperty(ref _quizQuestions, value);
    }

    private Question? _currentQuestion;

    public Question? CurrentQuestion
    {
        get => _currentQuestion;
        set => SetProperty(ref _currentQuestion, value);
    }

    private ObservableCollection<string> _answerOptions = new();

    public ObservableCollection<string> AnswerOptions
    {
        get => _answerOptions;
        set => SetProperty(ref _answerOptions, value);
    }



    private int _currentQuestionNumber;

    public int CurrentQuestionNumber
    {
        get => _currentQuestionNumber;
        set => SetProperty(ref _currentQuestionNumber, value);
    }

    private int _totalQuestions;

    public int TotalQuestions
    {
        get => _totalQuestions;
        set => SetProperty(ref _totalQuestions, value);
    }

    private int _timeRemaining;

    public int TimeRemaining
    {
        get => _timeRemaining;
        set => SetProperty(ref _timeRemaining, value);
    }


    private int _score;

    public int Score
    {
        get => _score;
        set => SetProperty(ref _score, value);
    }

    private void StartQuiz()
    {
        if (_mainWindowViewModel?.ActivePack == null)
            return;

        QuizQuestions = new ObservableCollection<Question>(
            _mainWindowViewModel.ActivePack.Questions);


        if (QuizQuestions.Count == 0)
            return;
       
        ShuffleQuestions();
        Score = 0;
        IsQuizFinished = false;
        HasAnswered = false;
        FeedbackMessage = string.Empty;

        IsQuizStarted = true;
        CurrentQuestionNumber = 1;
        CurrentQuestion = QuizQuestions[0];
        LoadAnswerOptions();

        TimeRemaining = _mainWindowViewModel.ActivePack.TimeLimitInSeconds;
        _timer?.Start();
    }
    private void ShuffleQuestions()
    {
        Random random = new Random();

        for (int i = QuizQuestions.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            (QuizQuestions[i], QuizQuestions[j]) =
                (QuizQuestions[j], QuizQuestions[i]);
        }
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining--;
        }


        if (TimeRemaining == 0)
        {
            _timer?.Stop();
            HasAnswered = true;

            if (CurrentQuestion != null)
            {
                FeedbackMessage = $"Tiden är slut! Rätt svar är: {CurrentQuestion.CorrectAnswer}";
            }
        }
    }

    private void LoadAnswerOptions()
    {
        if (CurrentQuestion == null)
            return;

        AnswerOptions = new ObservableCollection<string>
     {
        CurrentQuestion.CorrectAnswer,
        CurrentQuestion.IncorrectAnswers[0],
        CurrentQuestion.IncorrectAnswers[1],
        CurrentQuestion.IncorrectAnswers[2]
     };
        ShuffleAnswers();
    }

    private void ShuffleAnswers()
    {
        Random random = new Random();

        for (int i = AnswerOptions.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            (AnswerOptions[i], AnswerOptions[j]) =
                (AnswerOptions[j], AnswerOptions[i]);
        }
    }

    private void CheckAnswer(string? selectedAnswer)
    {


        if (CurrentQuestion == null || selectedAnswer == null)
            return;

        if (HasAnswered)
            return;

        HasAnswered = true;

        _timer?.Stop();

        if (selectedAnswer == CurrentQuestion.CorrectAnswer)
        {
            FeedbackMessage = "Rätt svar!";
            Score++;
        }
        else
        {
            FeedbackMessage = $"Fel svar! Rätt svar är: {CurrentQuestion.CorrectAnswer}";
        }
    }

    private void NextQuestion()
    {
        if (CurrentQuestionNumber >= QuizQuestions.Count)
        {
            IsQuizFinished = true;
            ResultMessage = $"Du fick {Score} rätt av {QuizQuestions.Count} frågor!";
            return;
        }

        CurrentQuestionNumber++;
        HasAnswered = false;

        CurrentQuestion = QuizQuestions[CurrentQuestionNumber - 1];

        LoadAnswerOptions();

        TimeRemaining = _mainWindowViewModel?.ActivePack?.TimeLimitInSeconds ?? 0;
        _timer?.Start();

        FeedbackMessage = string.Empty;
    }

    private string _feedbackMessage = string.Empty;

    public string FeedbackMessage
    {
        get => _feedbackMessage;
        set => SetProperty(ref _feedbackMessage, value);
    }

    private string _resultMessage = string.Empty;

    public string ResultMessage
    {
        get => _resultMessage;
        set => SetProperty(ref _resultMessage, value);
    }

    private bool _hasAnswered;

    public bool HasAnswered
    {
        get => _hasAnswered;
        set => SetProperty(ref _hasAnswered, value);
    }

    private bool _isQuizFinished;

    public bool IsQuizFinished
    {
        get => _isQuizFinished;
        set => SetProperty(ref _isQuizFinished, value);
    }

    private bool _isQuizStarted;

    public bool IsQuizStarted
    {
        get => _isQuizStarted;
        set => SetProperty(ref _isQuizStarted, value);
    }
}

