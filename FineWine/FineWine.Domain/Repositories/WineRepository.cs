using System;
using System.Collections.Generic;
using System.Linq;
using FineWine.Domain.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace FineWine.Domain.Repositories
{
    public class WineRepository : IWineRepository
    {
        public MongoDatabase MongoDatabase;
        public MongoCollection WinesCollection;
        public bool ServerIsDown = false;

        public WineRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017/");
            var server = mongoClient.GetServer();

            MongoDatabase = server.GetDatabase("FineWine");

            WinesCollection = MongoDatabase.GetCollection("Wine");

            try
            {
                MongoDatabase.Server.Ping();
                // Ping() method throws exception if not able to connect

            }
            catch (Exception ex)
            {
                ServerIsDown = true;
            }
        }

        private List<Wine> _wineList = new List<Wine>();

        public Wine GetLatestRioja()
        {
            var rioja = WinesCollection.AsQueryable<Wine>().Where(e => e.Name == "Riojo").First();
            return rioja;
        }

        public IEnumerable<Wine> GetAllWines()
        {
            if (ServerIsDown) return null;

            _wineList.Clear();
            var wines = WinesCollection.FindAs(typeof(Wine), Query.NE("Name", "null"));
            if (wines.Count() > 0)
            {
                foreach (Wine wine in wines)
                {
                    _wineList.Add(wine);
                }
            }

            var result = _wineList.AsQueryable();
            return result;
        }

        public Wine GetWinesById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id", "Employee Id is empty!");
            }
            var wine = (Wine)WinesCollection.FindOneAs(typeof(Wine), Query.EQ("_id", id));
            return wine;
        }


        public Wine Add(Wine wine)
        {
            WinesCollection.Save(wine);
            return wine;
        }

        public bool Update(string objectId, Wine wine)
        {
            UpdateBuilder updateBuilder = MongoDB.Driver.Builders.Update
                .Set("Name", wine.Name);
            WinesCollection.Update(Query.EQ("_id", objectId), updateBuilder);

            return true;
        }

        public bool Delete(string objectId)
        {
            WinesCollection.Remove(Query.EQ("_id", objectId));
            return true;
        }
    }
}
