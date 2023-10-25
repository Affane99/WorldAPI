using System;
using System.Collections.Generic;
using System.Text;

namespace World.Application.DTOs.Country
{
    public class CreateCountryDto
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Capitale { get; set; }
        public Guid ContinentId { get; set; }
    }
}
