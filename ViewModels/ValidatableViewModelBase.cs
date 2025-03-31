using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameLocalizationManagerApp.ViewModels;

/// <summary>
/// A <see cref="ViewModelBase"/> class supporting adding errors to properties
/// </summary>
public class ValidatableViewModelBase : ViewModelBase, INotifyDataErrorInfo
{
    private static readonly string[] NO_ERRORS = [];
    
    private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();

    /// <summary>
    /// Do any errors exists
    /// </summary>
    public bool HasErrors => _errorsByPropertyName.Count > 0;
    
    /// <summary>
    /// Invoked when any errors are added/deleted
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <summary>
    /// Returns the errors on a given property 
    /// </summary>
    public IEnumerable GetErrors(string? propertyName)
    {
        return _errorsByPropertyName.TryGetValue(propertyName, out var errors) ? errors : NO_ERRORS;
    }

    /// <summary>
    /// Adds an error to a property
    /// </summary>
    protected void AddError(string propertyName, string error)
    {
        if (_errorsByPropertyName.TryGetValue(propertyName, out var errorList))
        {
            if (!errorList.Contains(error))
            {
                errorList.Add(error);
            }
        }
        else
        {
            _errorsByPropertyName.Add(propertyName, new List<string>{ error });
        }
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
    
    /// <summary>
    /// Removes an error from a property
    /// </summary>
    protected void RemoveError(string propertyName)
    {
        if (_errorsByPropertyName.ContainsKey(propertyName))
        {
            _errorsByPropertyName.Remove(propertyName);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
    
    /// <summary>
    /// Validates a new value
    /// </summary>
    /// <param name="propertyName">Property name</param>
    /// <param name="value">The new value</param>
    /// <param name="validations">List of functions returning null if the validation passed and an error message otherwise</param>
    /// <typeparam name="T">Type of value</typeparam>
    /// <returns>True if all validations passed and false otherwise</returns>
    protected bool ValidateValue<T>(string propertyName, T? value, List<Func<T?, string?>> validations)
    {
        foreach (var validation in validations)
        {
            var error = validation(value);
            if (error != null)
            {
                AddError(propertyName, error);
                return false;
            }
        }
        
        RemoveError(propertyName);
        return true;
    }
}
