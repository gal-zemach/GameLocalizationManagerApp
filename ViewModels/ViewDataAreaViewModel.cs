using System;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using GameLocalizationManagerApp.Models.Data;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// View Model for the area that displays the contents of the localization file
/// </summary>
public partial class ViewDataAreaViewModel : ObservableObject
{
    private LocalizationData? _localizationData;

    /// <summary>
    /// Loads a new localization file
    /// </summary>
    public void LoadFile(string filePath)
    {
        try
        {
            var jsonContent = File.ReadAllText(filePath);
            _localizationData = JsonSerializer.Deserialize<LocalizationData>(jsonContent) ?? _localizationData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
