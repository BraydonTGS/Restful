using CommunityToolkit.Mvvm.ComponentModel;

namespace Restful.RequestsModule.Models
{
    public partial class Parameter : ObservableObject
    {
        [ObservableProperty]
        private bool _enabled;

        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        private string _value;

        [ObservableProperty]
        private string _description;
        public Parameter() { }

        public Parameter(string key, string value)
        {
            _key = key;
            _value = value;
        }
    }
}
