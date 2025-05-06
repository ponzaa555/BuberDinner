using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace BuberDinner.Application.Common.Interfaces.Error
{
    public class  DuplicateEmailError : IError
    {
        public List<IError> Reasons => throw new NotImplementedException();

        public string Message => throw new NotImplementedException();

        public Dictionary<string, object> Metadata => throw new NotImplementedException();
    }

}