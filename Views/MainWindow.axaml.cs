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
    private string m_LastFileDialogFolder;
    
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
        var startingFolder = string.IsNullOrEmpty(m_LastFileDialogFolder) ? Directory.GetCurrentDirectory() : m_LastFileDialogFolder;
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
            m_LastFileDialogFolder = Path.GetDirectoryName(files[0].Path.AbsolutePath);
        }
        
        return files;
    }
    
    ~MainWindow()
    {
        OnUnloaded();
    }
}
