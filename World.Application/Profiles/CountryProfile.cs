using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Country;
using World.Application.Features.Countries.Command.CreateCountry;
using World.Application.Features.Countries.Command.UpdateCountry;
using World.Domain;

namespace World.Application.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CreateCountryCommand, Country>();
            CreateMap<Country,CountryListDto>();
            CreateMap<UpdateCountryCommand, Country>();
        }
    }
}
