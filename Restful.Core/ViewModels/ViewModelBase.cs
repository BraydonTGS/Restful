using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Navigation;
using System.Windows;

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

        #region CloseApplicationWindow
        /// <summary>
        /// Close the Current Application Window by Window Type
        /// </summary>
        /// <param name="windowType"></param>
        public virtual void CloseApplicationWindow(Type windowType)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == windowType)
                {
                    window.Close();
                    break;
                }
            }
        }
        #endregion
    }
}
