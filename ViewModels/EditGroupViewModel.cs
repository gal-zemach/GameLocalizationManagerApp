using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLocalizationManagerApp.Models.Data;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// View Model for the area that allows editing an entry in the localization data
/// </summary>
public partial class EditGroupViewModel : ValidatableViewModelBase
{
    private Action<LocalizationGroup, LocalizationGroup> UpdateParentGroup;
    private LocalizationGroup? _selectedGroup;

    public EditGroupViewModel(Action<LocalizationGroup, LocalizationGroup> updateParentGroup)
    {
        UpdateParentGroup = updateParentGroup;
    }
    
    [ObservableProperty]
    private bool _isGroupSelectedForEdit;
    
    [ObservableProperty]
    private string? _key;
    
    [ObservableProperty]
    private string? _defaultValue;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddNewEntryCommand))]
    private string? _newKeyToAdd;

    public ObservableCollection<TranslationEntry> TranslationEntries { get; } = new();
    
    /// <summary>
    /// The group that is currently edited
    /// </summary>
    public LocalizationGroup? SelectedGroup
    {
        get => _selectedGroup;
        set
        {
            _selectedGroup = value;
            IsGroupSelectedForEdit = _selectedGroup != null;
            
            Key = value?.Key;
            DefaultValue = value?.Data.DefaultValue;
            LoadTranslationsFromDictionary(value?.Data.Translations);
        }
    }
    
    partial void OnKeyChanged(string? value)
    {
        ValidateKey(value);
    }
    
    [RelayCommand(CanExecute = nameof(CanSaveGroup))]
    private void SaveGroup()
    {
        var oldValue = SelectedGroup;
        var newValue = GetAsLocalizationGroup();
        UpdateParentGroup(oldValue, newValue);
    }
    
    private bool CanSaveGroup => !this.HasErrors && TranslationEntries.All(entry => !entry.HasErrors);

    private LocalizationGroup GetAsLocalizationGroup()
    {
        var data = new LocalizedString
        {
            DefaultValue = DefaultValue ?? string.Empty,
            Translations = TranslationEntries.ToDictionary(entry => entry.LanguageKey, entry => entry.Value)
        };
        
        return new LocalizationGroup(Key, data);
    }
    
    private void LoadTranslationsFromDictionary(Dictionary<string, string>? dict)
    {
        TranslationEntries.Clear();
        if (dict == null)
        {
            return;
        }
        
        foreach(var kvp in dict)
        {
            var newEntry = new TranslationEntry(kvp.Key, kvp.Value);
            TranslationEntries.Add(newEntry);
        }
        
        UpdateDuplicateDelegates();
    }

    private bool CanAddNewEntry() => !string.IsNullOrWhiteSpace(NewKeyToAdd) && 
                                     TranslationEntries.All(entry => entry.LanguageKey != NewKeyToAdd);

    [RelayCommand(CanExecute = nameof(CanAddNewEntry))]
    private void AddNewEntry()
    {
        var newEntry = new TranslationEntry(NewKeyToAdd, "");
        TranslationEntries.Add(newEntry);
        UpdateDuplicateDelegates();
    }
    
    [RelayCommand]
    private void RemoveEntry(TranslationEntry toRemove)
    {
        TranslationEntries.Remove(toRemove);
        UpdateDuplicateDelegates();
    }

    private void UpdateDuplicateDelegates()
    {
        foreach (var entry in TranslationEntries)
        {
            entry.IsDuplicateKey = key =>
                TranslationEntries.Any(other => other != entry && other.LanguageKey.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
    
    private bool ValidateKey(string? key)
    {
        List<Func<string?, string?>> validations =
        [
            (value) => string.IsNullOrWhiteSpace(value) ? "Key cannot be empty." : null,
            (value) => SelectedGroup.IsDuplicateKey?.Invoke(value) == true ? "Duplicate key exists." : null
        ];
        
        return ValidateValue(nameof(_selectedGroup.Key), key, validations);
    }
}
