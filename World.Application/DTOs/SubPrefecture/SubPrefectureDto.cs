using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Prefecture;

namespace World.Application.DTOs.SubPrefecture
{
    public class SubPrefectureDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PrefectureDto Prefecture { get; set; }
    }
}
