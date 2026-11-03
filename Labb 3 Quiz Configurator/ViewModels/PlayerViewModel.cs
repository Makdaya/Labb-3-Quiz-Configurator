using Labb_3_Quiz_Configurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_3_Quiz_Configurator.ViewModels;


internal class PlayerViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;

    public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
    }
}
