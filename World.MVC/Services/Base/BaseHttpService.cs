using World.MVC.Contracts;

namespace World.MVC.Services
{
    public class BaseHttpService
    {
        private IClient _client;
        private readonly ILocalStorageService _localStorageService;

        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation Errors have occured.", ValidationErrors = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The requested Item could not be found", Success = false };
            }
            else
            {
                return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
            }
        }

        protected void AddBaererToken()
        {
            if (_localStorageService.Exist("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Baerer", _localStorageService.GetStorageValue<string>("token"));
        }
    }
}
