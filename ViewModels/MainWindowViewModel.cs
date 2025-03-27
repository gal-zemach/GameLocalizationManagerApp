using System;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// Main ViewModel for the Game Localization Manager app
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    public JsonLoaderAreaViewModel JsonLoaderAreaViewModel { get; } = new();
    
    public ViewDataAreaViewModel ViewDataAreaViewModel { get; } = new();
    
    public EditEntryViewModel EditEntryViewModel { get; } = new();

    private IDisposable m_JsonPathDisposable;
    private IDisposable m_ChosenEntryDisposable;
    
    public MainWindowViewModel()
    {
        m_JsonPathDisposable = JsonLoaderAreaViewModel.SubscribeTo(nameof(JsonLoaderAreaViewModel.JsonPath),
            () => JsonLoaderAreaViewModel.JsonPath, 
            path => ViewDataAreaViewModel.LoadFile(path));
        
        // Subscribe editEntry to viewData.OnEntryChosen
        // m_ChosenEntryDisposable = 
    }
    
    ~MainWindowViewModel()
    {
        m_JsonPathDisposable?.Dispose();
        m_ChosenEntryDisposable?.Dispose();
    }
}
