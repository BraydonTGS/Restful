using Restful.Core.Constant;
using Restful.Core.ViewModels;

namespace Restful.WPF.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {


        public MainWindowViewModel()
        {
            Title = Constants.ApplicationTitle;
        }
    }
}
