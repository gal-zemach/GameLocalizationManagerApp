using System;
using System.Collections.ObjectModel;
using System.Linq;
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
    public ObservableCollection<LocalizationGroup> Entries { get; } = [];
    
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
    private LocalizationGroup _selectedGroup;
    
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
        LocalizationData.LocalizedStrings.Add(NewKeyToAdd, new LocalizedString());
        Entries.Add(new LocalizationGroup(NewKeyToAdd, LocalizationData.LocalizedStrings[NewKeyToAdd]));
        NewKeyToAdd = null;

        UpdateDuplicateDelegates();
    }

    [RelayCommand]
    private void RemoveItem(LocalizationGroup item)
    {
        Entries.Remove(item);
        UpdateDuplicateDelegates();
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
            Entries.Add(new LocalizationGroup(kvp.Key, kvp.Value));
        }

        UpdateDuplicateDelegates();
    }

    /// <summary>
    /// Allows updating an existing localization entry
    /// </summary>
    public void UpdateEntry(LocalizationGroup oldValue, LocalizationGroup newValue)
    {
        var index = Entries.IndexOf(oldValue);

        try
        {
            Entries[index].Key = newValue.Key;
            Entries[index].Data = newValue.Data;

            UpdateDuplicateDelegates();
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void UpdateDuplicateDelegates()
    {
        foreach (var entry in Entries)
        {
            entry.IsDuplicateKey = key =>
                Entries.Any(other => other != entry && other.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
}
