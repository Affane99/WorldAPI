using AutoMapper;
using World.MVC.Contracts;
using World.MVC.Models;

namespace World.MVC.Services
{
    public class ContinentService : BaseHttpService,IContinentService
    {
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public ContinentService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService):base(httpClient, localStorageService)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<Response<Guid>> CreateContinent(CreateContinentVM createContinentVM)
        {
            try
            {
                var response = new Response<Guid>();
                CreateContinentDto createContinentDto = _mapper.Map<CreateContinentDto>(createContinentVM);
                var apiResponse = await _httpClient.AddContinentAsync(createContinentDto);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteContinent(Guid id)
        {
            try
            {
                await _httpClient.DeleteContinentAsync(id);
                return new Response<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<List<ContinentVM>> GetAll(SearchDTO searchDTO)
        {
            var continents = await _httpClient.GetContinentListPageAsync(searchDTO);
            var mapper = _mapper.Map<List<ContinentVM>>(continents.Results);
            return mapper;
        }

        public async Task<ContinentVM> GetById(Guid id)
        {
            var continent = await _httpClient.GetContinentAsync(id);
            return _mapper.Map<ContinentVM>(continent);
        }

        public async Task<Response<Guid>> UpdateContinent(ContinentVM updateContinentVM)
        {
            try
            {
                UpdateContinentDto updateContinent = _mapper.Map<UpdateContinentDto>(updateContinentVM);
                var response = new Response<Guid>();
                var apiResponse = await _httpClient.UpdateContinentAsync(updateContinent);
                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
