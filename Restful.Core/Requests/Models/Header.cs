using CommunityToolkit.Mvvm.ComponentModel;

namespace Restful.Core.Requests.Models
{
    public partial class Header : ObservableObject
    {
        [ObservableProperty]
        private bool _enabled;

        [ObservableProperty]
        private string _key = string.Empty;

        [ObservableProperty]
        private string _value = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        public Header() { }
        public Header(string key, string value, bool enabled = false)
        {
            _key = key;
            _value = value;
            _enabled = enabled;
        }
    }
}
