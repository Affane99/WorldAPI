using System;
using System.Collections.Generic;
using System.Text;

namespace World.Application.Models.Identity
{
    public class JwtSettings
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
