using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SehirRehberiEnginD.API.Controllers;
using SehirRehberiEnginD.API.Dtos;
using SehirRehberiEnginD.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiEnginD.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // direkt olarak isimleri eşleşenleri map edecek
            CreateMap<City, CityForListDto>().ForMember(dest=> dest.PhotoUrl,opt=>
            {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            }); // photo url i map et. nerden city deki (src kaynaktaki) fotolardan is main olanın urlini al.

            CreateMap<City, CityForDetailDto>();

            CreateMap<Photo, PhotoForCreationDto>();
            CreateMap<PhotoForReturnDto, Photo>();
        }
    }
}
