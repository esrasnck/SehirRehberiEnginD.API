using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using SehirRehberiEnginD.API.Data;
using SehirRehberiEnginD.API.Dtos;
using SehirRehberiEnginD.API.Models;

namespace SehirRehberiEnginD.API.Controllers
{
    [Route("api/Cities")]
    [Produces("application/json")]
    
    public class CitiesController : Controller
    {
        IAppRepository _appRepository;
        IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {

            _appRepository = appRepository;
            _mapper = mapper;
        }

        public ActionResult GetCities()
        {
            //var cities = _appRepository.GetCities().Select(c=> new CityForListDto { Description = c.Description, Name = c.Name, Id=c.Id, PhotoUrl=c.Photos.FirstOrDefault(p=> p.IsMain==true).Url}).ToList();
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);
        }

        [HttpGet]
        [Route("detail")]
        public ActionResult GetCityById(int id)
        {
       
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }

        [HttpGet]
        [Route("Photos")]
        public ActionResult GetPhotosByCity(int cityId)
        {
            var photos = _appRepository.GetPhotosByCity(cityId);
            return Ok(photos);
        }

    }
}
