using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class ServiceStatus<TResponse>
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string? InnerMessage { get; set; }
        public TResponse Object { get; set; }
    }
}
