using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLocalizationManagerApp.Models.Data;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// ViewModel for the window area where a Json file is loaded/saved
/// </summary>
public partial class JsonLoaderAreaViewModel : ViewModelBase
{
    public Func<Task<IReadOnlyList<IStorageFile>>>? FileDialogRequested;
    
    [ObservableProperty]
    private string? _jsonPath;
    
    [ObservableProperty]
    private LocalizationData? _localizationData;
    
    [RelayCommand]
    private async Task SelectFileAsync()
    {
        if (FileDialogRequested != null)
        {
            var result = await FileDialogRequested.Invoke();
            if (result != null && result.Count > 0)
            {
                JsonPath = result[0].Path.AbsolutePath;
            }
        }
    }

    [RelayCommand]
    private void LoadJson()
    {
        if (_jsonPath == null)
        {
            return;
        }
        
        try
        {
            var jsonContent = File.ReadAllText(_jsonPath);
            LocalizationData = JsonSerializer.Deserialize<LocalizationData>(jsonContent) ?? LocalizationData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
