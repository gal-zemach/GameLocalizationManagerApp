using System;
using GameLocalizationManagerApp.Common;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// Main ViewModel for the Game Localization Manager app
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    public JsonLoaderAreaViewModel JsonLoaderAreaViewModel { get; } = new();
    
    public ViewDataAreaViewModel ViewDataAreaViewModel { get; } = new();
    
    public EditEntryViewModel EditEntryViewModel { get; } = new();

    private readonly IDisposable _loadJsonDisposable;
    private readonly IDisposable _chosenEntryDisposable;
    
    public MainWindowViewModel()
    {
        _loadJsonDisposable = JsonLoaderAreaViewModel.SubscribeTo(nameof(JsonLoaderAreaViewModel.LocalizationData),
            () => JsonLoaderAreaViewModel.LocalizationData, 
            data => ViewDataAreaViewModel.LocalizationData = data);
        
        // Subscribe editEntry to viewData.OnEntryChosen
        // m_ChosenEntryDisposable = 
    }
    
    ~MainWindowViewModel()
    {
        _loadJsonDisposable?.Dispose();
        _chosenEntryDisposable?.Dispose();
    }
}
