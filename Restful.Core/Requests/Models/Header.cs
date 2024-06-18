﻿using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Base;
using System.Diagnostics;

namespace Restful.Core.Requests.Models
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

        private Guid RequestId { get; set; }

        public Header() { }

        public Header(string key, string value, bool enabled = false)
        {
            Key = key;
            Value = value;
            Enabled = enabled;
        }

        public override string ToString() => $"{Key}={Value}";

        private string GetDebuggerDisplay() => $"{Key}={Value}";
    }
}
