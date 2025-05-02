using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtSetting
    {
        public const string SectionName = "JwtSettings";
        public string Secret {get; set;} = null!;
        public int ExpiryMinuts {get; set;}
        public string Issuer {get; set;} = null!;
        public string Audience {get; set;} = null!;
    }
}