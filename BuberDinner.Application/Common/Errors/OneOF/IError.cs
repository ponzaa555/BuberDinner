using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors.OneOF
{
    public interface IError
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; } 
    }
}