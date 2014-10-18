using FineWine.Domain.Model;
using FineWine.Domain.Repositories;

namespace FineWine.Domain.Services
{
    public class WineService : IWineService
    {
        private readonly IWineRepository _wineRepository;

        public WineService(IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        public Wine GetLatestRioja()
        {
            var latestRioja = _wineRepository.GetLatestRioja();
            return latestRioja;
        }
    }
}
