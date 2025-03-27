using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameLocalizationManagerApp.Models.Data
{
    /// <summary>
    /// A collection of localization strings
    /// </summary>
    [Serializable]
    public class LocalizationData
    {
        public Dictionary<string, LocalizedString> LocalizedStrings { get; set; }

        [JsonConstructor]
        public LocalizationData(Dictionary<string, LocalizedString> localizedStrings)
        {
            LocalizedStrings = localizedStrings;
        }
    }
}
