using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLocalizationManagerApp.Models.Data;
using GameLocalizationManagerApp.Services;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// ViewModel for the window area where a Json file is loaded/saved
/// </summary>
public partial class JsonLoaderAreaViewModel : ViewModelBase
{
    private Func<LocalizationData>? _getDataToSave;
    
    public JsonLoaderAreaViewModel(Func<LocalizationData> getDataToSaveFunc)
    {
        _getDataToSave = getDataToSaveFunc;
    }
    
    /// <summary>
    /// Invoked when this class requests that the file dialog open
    /// </summary>
    public Func<Task<IReadOnlyList<IStorageFile>>>? FileDialogRequested;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoadJsonCommand), nameof(SaveJsonCommand))]
    private string? _jsonPath;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveJsonCommand))]
    private LocalizationData? _localizationData;

    private bool DataWasLoaded => LocalizationData != null;
    
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

    private bool CanLoadJson => !string.IsNullOrWhiteSpace(JsonPath);
    
    [RelayCommand(CanExecute = nameof(CanLoadJson))]
    private async Task LoadJson()
    {
        LocalizationData = await LocalizationManagerFileService.LoadJsonFile<LocalizationData>(JsonPath);
    }
    
    private bool CanSaveJson => !string.IsNullOrWhiteSpace(JsonPath) && DataWasLoaded;
    
    [RelayCommand(CanExecute = nameof(CanSaveJson))]
    private async Task SaveJson()
    {
        var dataToSave = _getDataToSave?.Invoke();
        await LocalizationManagerFileService.SaveJsonFile(JsonPath, dataToSave);
    }
}
