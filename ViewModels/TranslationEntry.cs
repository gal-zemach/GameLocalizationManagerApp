using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// View Model for a single translation entry
/// </summary>
public partial class TranslationEntry : ValidatableViewModelBase
{
    public TranslationEntry(string key, string? value)
    {
        LanguageKey = key;
        Value = value ?? string.Empty;
    }
    
    [ObservableProperty]
    private string _languageKey;
    
    [ObservableProperty]
    private string _Value;
    
    /// <summary>
    /// Returns true if the <see cref="_languageKey"/> field is a duplicate of another key in the parent dictionary
    /// </summary>
    public Func<string, bool>? IsDuplicateKey { get; set; }
    
    partial void OnLanguageKeyChanged(string oldValue, string newValue)
    {
        ValidateKey(newValue);
    }
    
    private bool ValidateKey(string? key)
    {
        List<Func<string?, string?>> validations =
        [
            (value) => string.IsNullOrWhiteSpace(value) ? "Key cannot be empty." : null,
            (value) => IsDuplicateKey?.Invoke(value) == true ? "Duplicate key exists." : null
        ];
        
        return ValidateValue(nameof(LanguageKey), key, validations);
    }
}
