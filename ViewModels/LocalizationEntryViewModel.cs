using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLocalizationManagerApp.Models.Data;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// View Model for a single entry in the View Area
/// </summary>
public partial class LocalizationEntryViewModel : ViewModelBase
{
    [ObservableProperty] 
    private string _key;
    
    [ObservableProperty]
    private LocalizedString _data;

    public string LanguageCodes =>
        Data.Translations is not null ? string.Join(", ", Data.Translations.Keys) : string.Empty;
    
    public LocalizationEntryViewModel(string key, LocalizedString value)
    {
        Key = key;
        Data = value;
    }
    
    // public void AddTranslation(string lang, string value)
    // {
    //     Translations[lang] = value;
    //     OnPropertyChanged(nameof(LanguageCodes));
    // }
    //
    // public void RemoveTranslation(string lang)
    // {
    //     if (Translations.Remove(lang))
    //     {
    //         OnPropertyChanged(nameof(LanguageCodes));
    //     }
    // }
}
