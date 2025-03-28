using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLocalizationManagerApp.Models.Data;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// View Model for the area that displays the contents of the localization file
/// </summary>
public partial class ViewDataAreaViewModel : ViewModelBase
{
    /// <summary>
    /// ViewModels for each localization entry row
    /// </summary>
    public ObservableCollection<LocalizationEntryViewModel> Entries { get; } = [];
    
    /// <summary>
    /// The data presented in the window
    /// </summary>
    public LocalizationData? LocalizationData
    {
        get => _localizationData;
        set
        {
            _localizationData = value;
            IsDataLoaded = _localizationData != null;
            IsDataEmpty = _localizationData?.LocalizedStrings.Count == 0;
            
            SetToEntriesObservable(_localizationData);
        }
    }
    
    private LocalizationData? _localizationData;

    [ObservableProperty]
    private bool _isDataLoaded;
    
    [ObservableProperty]
    private bool _isDataEmpty;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))]
    private string? _newKeyToAdd;
    
    private bool CanAddNewItem => !string.IsNullOrEmpty(NewKeyToAdd) && 
                                  LocalizationData != null && !LocalizationData.LocalizedStrings.ContainsKey(NewKeyToAdd);
    
    /// <summary>
    /// This command is used to add a new Item
    /// </summary>
    [RelayCommand (CanExecute = nameof(CanAddNewItem))]
    private void AddItem()
    {
        Entries.Add(new LocalizationEntryViewModel(NewKeyToAdd, new LocalizedString()));
        NewKeyToAdd = null;
    }

    [RelayCommand]
    private void RemoveItem(LocalizationEntryViewModel item)
    {
        Entries.Remove(item);
    }
    
    private void SetToEntriesObservable(LocalizationData? newData)
    {
        Entries.Clear();
        if (newData == null)
        {
            return;
        }
        
        foreach (var kvp in newData.LocalizedStrings)
        {
            Entries.Add(new LocalizationEntryViewModel(kvp.Key, kvp.Value));
        }
    }
}
