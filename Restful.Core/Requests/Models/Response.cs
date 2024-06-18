using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Base;
using System.Diagnostics;

namespace Restful.Core.Requests.Models
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class Response : ModelBase<Guid>
    {
        [ObservableProperty]
        private string? _result;

        public Guid RequestId { get; set; }

        private string GetDebuggerDisplay() => $"Results: {Result}";
    }
}
