using Avalonia;
using Avalonia.Controls;

namespace GameLocalizationManagerApp.Common.Controls;

public partial class LabeledControl : ContentControl
{
    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<LabeledControl, string>(nameof(Label), "Label");

    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
}
