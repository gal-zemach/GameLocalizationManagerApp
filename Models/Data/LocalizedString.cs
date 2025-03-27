using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameLocalizationManagerApp.Models.Data
{
    /// <summary>
    /// A string that can have localized versions 
    /// </summary>
    [Serializable]
    public class LocalizedString
    {
        /// <summary>
        /// Default string value 
        /// </summary>
        [JsonPropertyName("DefaultValue")]
        public string DefaultValue { get; set; } = string.Empty;

        [JsonPropertyName("Translations")]
        public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// Add a new translation
        /// </summary>
        /// <param name="key">Language code</param>
        /// <param name="value">Translated version</param>
        public void SetTranslation(string key, string value)
        {
            Translations[key] = value;
        }
        
        /// <summary>
        /// Given a language key, returns the localized string value
        /// </summary>
        /// <param name="key">A language key</param>
        /// <returns>Localized string value or the default value if the key doesn't exist</returns>
        public string GetLocalizedString(string key)
        {
            return Translations.GetValueOrDefault(key, DefaultValue);
        }
    }
}
