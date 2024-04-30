using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Restful.RequestsModule.Views
{
    /// <summary>
    /// Interaction logic for RequestDetailsView.xaml
    /// </summary>
    public partial class RequestDetailsView : UserControl
    {
        public RequestDetailsView()
        {
            InitializeComponent();
            responseTextBox.SyntaxHighlighting
                .GetNamedColor("String").Foreground = new SimpleHighlightingBrush(Colors.Green);
        }
    }
}
