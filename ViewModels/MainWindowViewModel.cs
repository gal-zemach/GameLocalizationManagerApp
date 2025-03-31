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
    
    public EditGroupViewModel EditGroupViewModel { get; }

    private readonly IDisposable _loadJsonDisposable;
    private readonly IDisposable _chosenEntryDisposable;
    
    public MainWindowViewModel()
    {
        EditGroupViewModel = new EditGroupViewModel(ViewDataAreaViewModel.UpdateEntry);
        
        _loadJsonDisposable = JsonLoaderAreaViewModel.SubscribeTo(nameof(JsonLoaderAreaViewModel.LocalizationData),
            () => JsonLoaderAreaViewModel.LocalizationData, 
            data => ViewDataAreaViewModel.LocalizationData = data);
        
        _chosenEntryDisposable = ViewDataAreaViewModel.SubscribeTo(nameof(ViewDataAreaViewModel.SelectedGroup), 
            () => ViewDataAreaViewModel.SelectedGroup,
            data => EditGroupViewModel.SelectedGroup = data);
    }
    
    ~MainWindowViewModel()
    {
        _loadJsonDisposable?.Dispose();
        _chosenEntryDisposable?.Dispose();
    }
}
