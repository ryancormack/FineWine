using System;
using System.Collections.Generic;
using System.Linq;
using FineWine.Domain.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace FineWine.Domain.Repositories
{
    public class WineRepository
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

        private Wine[] _testWineData = new Wine[]
        {
            new Wine()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Castillo San Lorenzo"
            },
            new Wine()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Riojo"
            }
        };

        private List<Wine> _wineList = new List<Wine>();

        public IEnumerable<Wine> GetAllWines()
        {
            if (ServerIsDown) return null;
 
            if (Convert.ToInt32(WinesCollection.Count()) > 0)
            {
                _wineList.Clear();
                var employees = WinesCollection.FindAs(typeof (Wine), Query.NE("FirstName", "null"));
                if (employees.Count() > 0)
                {
                    foreach (Wine employee in employees)
                    {
                        _wineList.Add(employee);
                    }
                }
            }
            else
            {
                WinesCollection.RemoveAll();
                foreach (var employee in _testWineData)
                {
                    _wineList.Add(employee);
 
                    Add(employee); // add data to mongo db also
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
            var employee = (Wine)WinesCollection.FindOneAs(typeof(Wine), Query.EQ("_id", id));
            return employee;
        }


        public Wine Add(Wine wine)
        {
            if (string.IsNullOrEmpty(wine.Id))
            {
                wine.Id = Guid.NewGuid().ToString();
            }
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
