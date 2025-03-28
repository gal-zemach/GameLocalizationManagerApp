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
    private string _defaultValue;
    
    [ObservableProperty]
    private Dictionary<string, string> _translations;

    public string LanguageCodes => Translations is not null ? string.Join(", ", Translations.Keys) : string.Empty;
    
    public LocalizationEntryViewModel(string key, LocalizedString value)
    {
        Key = key;
        DefaultValue = value.DefaultValue;
        Translations = value.Translations;
    }

    /// <summary>
    /// Returns a <see cref="LocalizedString"/> with the ViewModel's value 
    /// </summary>
    /// <returns></returns>
    public LocalizedString GetValue()
    {
        return new LocalizedString
        {
            DefaultValue = this.DefaultValue,
            Translations = this.Translations
        };
    }
    
    public void AddTranslation(string lang, string value)
    {
        Translations[lang] = value;
        OnPropertyChanged(nameof(LanguageCodes));
    }

    public void RemoveTranslation(string lang)
    {
        if (Translations.Remove(lang))
        {
            OnPropertyChanged(nameof(LanguageCodes));
        }
    }
}
