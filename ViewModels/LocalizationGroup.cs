using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLocalizationManagerApp.Models.Data;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// View Model for a single localization group in the View Area
/// </summary>
public partial class LocalizationGroup : ViewModelBase
{
    [ObservableProperty] 
    private string _key;
    
    [ObservableProperty]
    private LocalizedString _data;

    public string LanguageCodes =>
        Data.Translations is not null ? string.Join(", ", Data.Translations.Keys) : string.Empty;
    
    public LocalizationGroup(string key, LocalizedString value)
    {
        Key = key;
        Data = value;
    }

    partial void OnDataChanged(LocalizedString value)
    {
        OnPropertyChanged(nameof(LanguageCodes));
    }
    
    /// <summary>
    /// Returns true if the <see cref="Key"/> field is a duplicate of another key in the parent dictionary
    /// </summary>
    public Func<string, bool>? IsDuplicateKey { get; set; }
}
