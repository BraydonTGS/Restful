using ICSharpCode.AvalonEdit.Highlighting;
using System.Linq;
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

            //var highlight = responseTextBox.SyntaxHighlighting;

            //if (highlight != null)
            //{
            //    // Find the Regex named color in the highlighting definition
            //    var regexColor = highlight.NamedHighlightingColors.FirstOrDefault(x => x.Name == "String");

            //    if (regexColor != null)
            //    {
            //        // Change the foreground color to green
            //        regexColor.Foreground = new SimpleHighlightingBrush(Colors.Green);
            //    }

            //    // Reapply the highlighting definition to ensure the update takes effect
            //    responseTextBox.SyntaxHighlighting = highlight;
            //}

        }
    }
}
