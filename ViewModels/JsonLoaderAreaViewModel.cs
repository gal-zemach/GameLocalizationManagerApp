using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// ViewModel for the window area where a Json file is loaded/saved
/// </summary>
public partial class JsonLoaderAreaViewModel : ObservableObject
{
    public Func<Task<IReadOnlyList<IStorageFile>>> FileDialogRequested;
    
    [ObservableProperty]
    private string? jsonPath;
    
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
}
