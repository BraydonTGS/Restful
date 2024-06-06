using CommunityToolkit.Mvvm.ComponentModel;

namespace Restful.Core.Requests.Models
{
    public partial class Parameter : ObservableObject
    {
        [ObservableProperty]
        private bool _enabled;

        [ObservableProperty]
        private string _key = string.Empty;

        [ObservableProperty]
        private string _value = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;
        public Parameter() { }

        public Parameter(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public bool CanAddToUrl() => Enabled && !string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Value);

        public override string ToString() => $"{Key}={Value}";
    }
}
