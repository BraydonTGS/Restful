using CommunityToolkit.Mvvm.ComponentModel;

namespace Restful.RequestsModule.Models
{
    public partial class Header : ObservableObject
    {
        [ObservableProperty]
        private bool _enabled;

        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        private string _value;

        [ObservableProperty]
        private string _description;

        public Header() { }
        public Header(string key, string value, bool enabled = false)
        {
            _key = key;
            _value = value;
            _enabled = enabled;
        }
    }
}
