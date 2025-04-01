using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameLocalizationManagerApp.Services;

public static class LocalizationManagerFileService
{
    /// <summary>
    /// Loads & Deserializes the JSON file at the given path 
    /// </summary>
    public static async Task<T?> LoadJsonFile<T>(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return default;
        }
        
        try
        {
            var jsonContent = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(jsonContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task SaveJsonFile<T>(string path, T value)
    {
        var directory = Path.GetDirectoryName(path);
        if (directory == null)
        {
            throw new Exception("Directory path is empty.");
        }
        
        Directory.CreateDirectory(directory);
        // JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });

        await using var fs = File.Create(path);
        await JsonSerializer.SerializeAsync(fs, value);
    }
}
