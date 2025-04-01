# Game Localization Manager

A desktop application (built with [Avalonia](https://avaloniaui.net/)) designed to help manage localization keys, default strings, and per-language translations for game projects. 
It allows you to **load** a JSON file, **view** and **edit** existing localization data, **create** new localization entries, and **save** everything back to JSON.

This was built as my first Avalonia app, trying to learn the syntax and practice MVVM.


![Game Localization Manager UI](./path/to/screenshot.png)

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Future Improvements](#future-improvements)
- [License](#license)

---

## Features
1. **Load and Parse JSON**
    - Reads a JSON file containing localization entries.
    - Displays a list of keys, their default values, and the languages available.

2. **View & Edit Localization Entries**
    - Select an entry to see its details (key, default value, translations).
    - Modify any field and apply your changes in real time.

3. **Add / Remove Languages**
    - For each entry, you can add a new language translation or delete an existing one.

4. **Create / Delete Entries**
    - Add new localization keys along with default text.
    - Remove unneeded entries directly from the list.

5. **Save Back to JSON**
    - When changes are made, save them to the JSON file to keep everything up-to-date.

6. **Dark Theme UI** (as seen in the screenshot)
    - Designed with a dark color scheme that is friendly for extended use.

---

## Technologies Used
- **C#** and **.NET**
- **[Avalonia UI](https://avaloniaui.net/)** for cross-platform desktop UI
- **System.Text.Json** for JSON serialization/deserialization
- **MVVM** architectural pattern (ViewModel-View-Binding)

---

## Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/en-us/download) installed.
- A JSON file with a structure similar to this (adjust fields as needed):
  ```json
  {
  "LocalizedStrings": {
    "greeting_text": {
      "DefaultValue": "Hi There!",
      "Translations": {
        "en": "Hi There!",
        "fr": "Salut !",
        "es": "¡Hola!"
      }
    },
    "goodbye_text": {
      "DefaultValue": "See You Next Time!",
      "Translations": {
        "en": "See You Next Time!",
        "fr": "À la prochaine !",
        "es": "¡Hasta la próxima!",
        "de": "Bis zum nächsten Mal!"
      }
    }
  }
}

---

## Usage
1. **Load JSON File**
   - In the top section, click the "..." button to select your localization JSON file path.
   - Click *Load JSON* to parse and display its contents.

2. **View/Select Entries**
   - The second section “File Content” lists each localization key, its default text, and a quick summary of available languages.
   - Selecting an entry shows its full details at the bottom panel.

3. **Edit Details**
   - In the “Entry Details” section, you can modify the key name, default value, or any individual translation.
   - When you’re done, click *Update Data* to apply changes in memory.

4. **Save JSON**
   - When you're done with all the changes to the file, click *Save JSON* at the top to apply the data to the original file.

---

## Usage
```
GameLocalizationManagerApp/
│
├─ Assets/
│   └─ Sample.json           # Sample JSON file
│
├─ Models/
│   └─ Data
│       └─ LocalizationData.cs   # Represents the full localization file
│       └─ LocalizedString.cs   # Represents a single localized string
│
├─ ViewModels/
│   └─ MainWindowViewModel.cs  # Main VM, hold the 3 main view models
│   └─ ValidatableViewModelBase.cs  # Base class for VMs that require validation
│   └─ JsonLoaderAreaViewModel.cs  # 1st section VM
│   └─ ViewDataAreaViewModel.cs  # 2nd section VM
│   └─ LocalizationGroup.cs  # Represents a single localization group (aka key + translations)
│   └─ EditGroupViewModel.cs  # 3rd section VM
│   └─ TranslationEntry.cs  # Represents a single translation entry (aka language code + value in that language)
│
├─ Views/
│   └─ MainWindow.axaml         # Main window view
│   └─ MainWindow.axaml.cs      # Partial code-behind for MainWindow, specifically opens the file dialog
│   └─ JsonLoaderControl.axaml  # Json load/save area view (1)
│   └─ ViewDataControl.axaml    # View localization data view (2)
│   └─ EditGroupControl.axaml   # Edit a single localization group view (3)
│
├─ Themes/
│   └─ Generic.axaml   # Holds common views
│
├─ Services/
│   └─ LocalizationManagerFileService.cs   # Used to Save/Load files.
│
└─ Program.cs                 # Application entry point
```

---

## Future Improvements
- **Improve common Views/VMs** to reduce code duplication (f.e. in validating key with dictionaries).
- **Add "Unsaved Changes" dialog** when moving between entries while changes have been made.
- **Improve Save flow** need to find a better place for it + allow saving to a new file.
- **Make section 3 a foldout** Instead of section 3, make it so clicking a group creates section 3 under the entry and above the next one.
- **Standardize styling** to reduce numeric styling throughout the code (f.e. have "Header" style instead of writing fontSize+bold).
- **Better Undo/Redo support** currently these only work for some actions so can't be trusted.

---

## License
This project is provided under the MIT License.
