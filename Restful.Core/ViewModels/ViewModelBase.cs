using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Navigation;

namespace Restful.Core.ViewModels
{
    public partial class ViewModelBase : ObservableObject, IDestructible
    {
        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;

        public ViewModelBase() { }

        public virtual void Destroy() { }
        protected virtual void HandleException(Exception exception) { }
        protected virtual void TaskCompleted() { }
    }
}
