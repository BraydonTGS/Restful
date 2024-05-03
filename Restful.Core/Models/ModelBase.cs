using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restful.Core.Models
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

    }
}
