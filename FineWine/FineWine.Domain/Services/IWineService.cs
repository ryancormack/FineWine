using FineWine.Domain.Model;

namespace FineWine.Domain.Services
{
    public interface IWineService : IService
    {
        Wine GetLatestRioja();
    }
}