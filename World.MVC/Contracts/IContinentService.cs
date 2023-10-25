using World.MVC.Models;
using World.MVC.Services;

namespace World.MVC.Contracts
{
    public interface IContinentService
    {
        Task<List<ContinentVM>> GetAll(SearchDTO searchDTO);
        Task<ContinentVM> GetById(Guid id);
        Task<Response<Guid>> CreateContinent(CreateContinentVM createContinentVM);
        Task<Response<Guid>> UpdateContinent(ContinentVM updateContinentVM);
        Task<Response<Guid>> DeleteContinent(Guid id);
    }
}
