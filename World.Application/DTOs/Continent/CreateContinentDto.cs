using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Common;

namespace World.Application.DTOs.Continent
{
    public class CreateContinentDto : IContinentDto
    {
        public string Name { get; set; }
    }
}
