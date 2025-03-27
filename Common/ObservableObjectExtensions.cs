using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLocalizationManagerApp.Common;

namespace GameLocalizationManagerApp.Common;

/// <summary>
/// Extensions class for <see cref="ObservableObject"/>
/// </summary>
public static class ObservableObjectExtensions
{
    /// <summary>
    /// Subscribes an action to a property's changes
    /// </summary>
    /// <param name="observableObject">Contains the property</param>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="getPropertyValue">Function for retrieving the property's value</param>
    /// <param name="action">Will be called when the property changes</param>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <returns>IDisposable that unsubscribes the action</returns>
    public static IDisposable SubscribeTo<T>(this ObservableObject observableObject, string propertyName, Func<T> getPropertyValue, Action<T> action)
    {
        PropertyChangedEventHandler handler = (_, args) =>
        {
            if (args.PropertyName == propertyName)
            {
                action.Invoke(getPropertyValue());
            }
        };
        
        observableObject.PropertyChanged += handler;
        
        return new Disposable(() => observableObject.PropertyChanged -= handler);
    }
}
