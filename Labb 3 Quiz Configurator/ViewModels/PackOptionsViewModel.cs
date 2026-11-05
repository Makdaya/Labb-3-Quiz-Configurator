using System;
using System.Collections.Generic;
using System.Text;
using Labb_3_Quiz_Configurator.Models;

namespace Labb_3_Quiz_Configurator.ViewModels
{
    internal class PackOptionsViewModel : ViewModelBase
    {
        private readonly QuestionPackViewModel _model;
        private string _name = string.Empty;
        private Difficulty _difficulty;
        private int _timeLimitInSeconds;

        public PackOptionsViewModel(QuestionPackViewModel model)
        {
            _model = model;
            _name = model.Name;
            _difficulty = model.Difficulty;
            _timeLimitInSeconds = model.TimeLimitInSeconds;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    _model.Name = value;
                }
            }
        }


        public Difficulty Difficulty
        {
            get => _difficulty;
            set
            {
                if (SetProperty(ref _difficulty, value))
                {
                    _model.Difficulty = value;
                }
            }
        }

        public int TimeLimitInSeconds
        {
            get => _timeLimitInSeconds;
            set
            {
                if (SetProperty(ref _timeLimitInSeconds, value))
                {
                    _model.TimeLimitInSeconds = value;
                }
            }
        }



        public Array DifficultyOptions =>
            Enum.GetValues(typeof(Labb_3_Quiz_Configurator.Models.Difficulty));


    }
    
}

