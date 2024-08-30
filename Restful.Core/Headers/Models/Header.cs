using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Base;
using System.Diagnostics;

namespace Restful.Core.Headers.Models
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class Header : ModelBase<Guid>
    {
        [ObservableProperty]
        private bool _enabled;

        [ObservableProperty]
        private string _key = string.Empty;

        [ObservableProperty]
        private string _value = string.Empty;

        public Guid RequestId { get; set; }

        public Header() { }

        public Header(
            string key,
            string value,
            bool enabled = false,
            bool isDefault = false)
        {
            Key = key;
            Value = value;
            Enabled = enabled;
            IsDefault = isDefault;
        }

        public override string ToString() => $"{Key}={Value}";

        private string GetDebuggerDisplay() => $"{Key}={Value}";
    }
}
