using System;
using System.Collections.Generic;

namespace GameLocalizationManagerApp.Models.Data;

/// <summary>
/// A collection of localization strings
/// </summary>
[Serializable]
public class LocalizationData
{
    public Dictionary<string, LocalizedString> LocalizedStrings { get; set; } = new();
}