using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using GameLocalizationManagerApp.ViewModels;

namespace GameLocalizationManagerApp.Views;

public partial class MainWindow : Window
{
    private string? _lastFileDialogFolder;
    
    public MainWindow()
    {
        InitializeComponent();
        
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is not MainWindowViewModel viewModel)
        {
            return;
        }
        
        viewModel.JsonLoaderAreaViewModel.FileDialogRequested += OpenFileDialogAsync;

    }
    
    private void OnUnloaded()
    {
        this.Loaded -= OnLoaded;

        if (this.DataContext is not MainWindowViewModel viewModel)
        {
            return;
        }
        
        viewModel.JsonLoaderAreaViewModel.FileDialogRequested -= OpenFileDialogAsync;
    }
    
    private async Task<IReadOnlyList<IStorageFile>> OpenFileDialogAsync()
    {
        // Use this window as the parent visual.
        var topLevel = TopLevel.GetTopLevel(this);
        var storageProvider = topLevel.StorageProvider;
        var startingFolder = string.IsNullOrEmpty(_lastFileDialogFolder) ? Directory.GetCurrentDirectory() : _lastFileDialogFolder;
        var startFolder = await storageProvider.TryGetFolderFromPathAsync(startingFolder);
        
        var jsonFileType = new FilePickerFileType("Only Json Files") 
        {
            Patterns = ["*.json"]
        };
        
        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select a File...",
            AllowMultiple = false,
            SuggestedStartLocation = startFolder, 
            FileTypeFilter = [jsonFileType]
        });

        if (files.Count > 0)
        {
            _lastFileDialogFolder = Path.GetDirectoryName(files[0].Path.AbsolutePath);
        }
        
        return files;
    }
    
    ~MainWindow()
    {
        OnUnloaded();
    }
}
