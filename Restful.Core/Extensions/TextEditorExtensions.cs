using ICSharpCode.AvalonEdit;
using System.Windows;

namespace Restful.Core.Extensions
{
    public class TextEditorExtensions
    {
        public static readonly DependencyProperty BoundTextProperty =
            DependencyProperty.RegisterAttached("BoundText", typeof(string),
                typeof(TextEditorExtensions), new FrameworkPropertyMetadata(default(string),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundTextChanged));

        public static string GetBoundText(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(BoundTextProperty);
        }

        public static void SetBoundText(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(BoundTextProperty, value);
        }

        private static void OnBoundTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = d as TextEditor;
            if (textEditor != null)
            {
                textEditor.Text = e.NewValue as string;
            }
        }
    }
}
