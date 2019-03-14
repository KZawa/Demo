using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Core.Repositories;
using WeatherApp.Infrastructure.DTOs;
using WeatherApp.Infrastructure.ExternalApiWeatherHandler;
using WeatherApp.Infrastructure.Helper;
using WeatherApp.Infrastructure.Settings;

namespace WeatherApp.Infrastructure.Services
{
    public class WeatherMeasureService : IWeatherMeasureService
    {
        private readonly IWeatherMeasureRepository _WeatherMeasureRepository;
        private readonly IMapper _mapper;
        private readonly IExternalApiWeatherHandler _externalApiWeatherHandler;
        private readonly ICityRepository _cityRepository;
        private readonly ExternalApiSettings _externalApiSettings;

        public WeatherMeasureService(IWeatherMeasureRepository weatherMeasureRepository, IMapper mapper, IExternalApiWeatherHandler externalApiWeatherHandler,
                                     ICityRepository cityRepository, IOptions<ExternalApiSettings> externalApiSettings)
        {
            _WeatherMeasureRepository = weatherMeasureRepository;
            _mapper = mapper;
            _externalApiWeatherHandler = externalApiWeatherHandler;
            _externalApiSettings = externalApiSettings.Value;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<WeatherMeasureDTO>> BrowseAsync(int city_id, DateTime maximumDate)
        {
            var weatherMeasureRecords = await _WeatherMeasureRepository.BrowseAsync(city_id, maximumDate);
            return _mapper.Map<IEnumerable<WeatherMeasureDTO>>(weatherMeasureRecords);
        }

        public async Task CreateAsync(WeatherMeasureDTO weatherMeasureDTO)
        {
            WeatherMeasures weatherMeasure = _mapper.Map<WeatherMeasures>(weatherMeasureDTO);
            await _WeatherMeasureRepository.CreateAsync(weatherMeasure);
        }

        public async Task DeleteAsync(int id)
        {
            await _WeatherMeasureRepository.DeleteAsync(id);
        }

        public async Task<WeatherMeasureDTO> GetAsync(int id)
        {
            WeatherMeasures WeatherMeasureRecord = await _WeatherMeasureRepository.GetAsync(id);
            return _mapper.Map<WeatherMeasureDTO>(WeatherMeasureRecord);
        }

        public async Task<WeatherMeasureDTO> GetAsync(int city_id, DateTime measurementDate)
        {
            WeatherMeasures WeatherMeasureRecord = await _WeatherMeasureRepository.GetAsync(city_id, measurementDate);
            return _mapper.Map<WeatherMeasureDTO>(WeatherMeasureRecord);
        }

        public async Task UpdateAsync(WeatherMeasureDTO weatherMeasureDTO)
        {
            WeatherMeasures weatherMeasure = _mapper.Map<WeatherMeasures>(weatherMeasureDTO);
            await _WeatherMeasureRepository.UpdateAsync(weatherMeasure);
        }

        public async Task UpdateAsync(string cityName)
        {
            var helper = new Helper.Helper();
            string url = _externalApiSettings.Path + cityName + _externalApiSettings.Appid;
            string json = await helper.GetWeatherDataFromURLAsync(url);
            var jObject = JObject.Parse(json);

            bool isCityExistsInDb = await CheckIfExistsCityRecordInDateBase(cityName);

            if (!isCityExistsInDb)
                await _cityRepository.CreateAsync(new Cities(cityName));

            List<WeatherMeasures> weatherMeasuresRecords = await _externalApiWeatherHandler.ConvertDataFromExternalApiToWeatherMeasureRecord(jObject, cityName);
            await CreateOrUpdateRecordList(weatherMeasuresRecords);
        }

        private async Task CreateOrUpdateRecordList(List<WeatherMeasures> weatherMeasures)
        {
            var tasks = new List<Task>();

            foreach (var record in weatherMeasures)
            {
                await CreareOrUpdateRecordAsync(record);
     //           tasks.Add(task);
            }
        //    await Task.WhenAll();
        }

        private async Task CreareOrUpdateRecordAsync(WeatherMeasures record)
        {
            bool isExists = await ChechIfExistsWeatherMeasureServiceRecordInDateBase(record.CityId, record.MeasureDate);

            if (isExists == true)
                await _WeatherMeasureRepository.UpdateAsync(record);           
            else
                await _WeatherMeasureRepository.CreateAsync(record);
        }

        private async Task<bool> ChechIfExistsWeatherMeasureServiceRecordInDateBase(int city_id, DateTime measurementDate)
        {
            var recordFromDb = await GetAsync(city_id, measurementDate);

            if (recordFromDb != null)
                return true;
            else return false;
        }

        private async Task<bool> CheckIfExistsCityRecordInDateBase(string cityName)
             => await _cityRepository.GetAsync(cityName) != null ? true : false;
        
    }
}
