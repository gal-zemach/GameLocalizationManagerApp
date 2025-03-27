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

    private readonly IDisposable _jsonPathDisposable;
    private readonly IDisposable _chosenEntryDisposable;
    
    public MainWindowViewModel()
    {
        _jsonPathDisposable = JsonLoaderAreaViewModel.SubscribeTo(nameof(JsonLoaderAreaViewModel.JsonPath),
            () => JsonLoaderAreaViewModel.JsonPath, 
            path => ViewDataAreaViewModel.LoadFile(path));
        
        // Subscribe editEntry to viewData.OnEntryChosen
        // m_ChosenEntryDisposable = 
    }
    
    ~MainWindowViewModel()
    {
        _jsonPathDisposable?.Dispose();
        _chosenEntryDisposable?.Dispose();
    }
}
