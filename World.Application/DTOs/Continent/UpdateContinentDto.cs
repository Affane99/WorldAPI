﻿using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Common;

namespace World.Application.DTOs.Continent
{
    public class UpdateContinentDto : BaseDto, IContinentDto
    {
        public string Name { get; set; }
    }
}
