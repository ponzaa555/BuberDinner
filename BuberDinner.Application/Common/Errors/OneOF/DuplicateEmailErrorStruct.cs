using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors.OneOF
{
    public record struct DuplicateEmailErrorStruct() : IError
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string Message => "Email already exists";
    }

}