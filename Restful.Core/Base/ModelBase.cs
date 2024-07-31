using CommunityToolkit.Mvvm.ComponentModel;

namespace Restful.Core.Base
{
    /// <summary>
    /// Base Model Class where you specify the type of Id
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public partial class ModelBase<TKey> : ObservableObject, IModel<TKey>
    {
        [ObservableProperty]
        private TKey? _id;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private bool _isDirty;

        [ObservableProperty]
        private bool _isDeleted;

        [ObservableProperty]
        private bool _isDefault;
    }
}
