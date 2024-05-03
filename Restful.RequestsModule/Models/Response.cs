using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Models;
using System;

namespace Restful.RequestsModule.Models
{
    public partial class Response : ModelBase<Guid>
    {
        [ObservableProperty]
        private string _result;
        public Guid RequestId { get; set; }
    }
}
