using System.Collections.Generic;
using FineWine.Domain.Model;

namespace FineWine.Domain.Repositories
{
    public interface IWineRepository : IRepository
    {
        Wine GetLatestRioja();
        IEnumerable<Wine> GetAllWines();
        Wine GetWinesById(string id);
        Wine Add(Wine wine);
        bool Update(string objectId, Wine wine);
        bool Delete(string objectId);
    }
}