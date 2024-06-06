using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Base;

namespace Restful.Core.Requests.Models
{
    public partial class Response : ModelBase<Guid>
    {
        [ObservableProperty]
        private string? _result;
        public Guid RequestId { get; set; }
    }
}
