using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using CommandTool.DocumentDb;
using Newtonsoft.Json;
using System.Linq;

namespace CommandTool
{
    public class PatientStore : IDisposable
    {
        static PatientStore()
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
            };
        }

        public PatientStore(IConfiguration configuration)
        {
            _client = new DocumentClient(
                new Uri(configuration.Get("DbUri")),
                configuration.Get("DbToken"));
        }

        public async Task<DocumentCollection> Setup()
        {
            Console.WriteLine("Steup");

            var databaseId = "PatientDb";
            var collectionId = "Patients";

            _database = _client.CreateDatabaseQuery()
                               .Where(d => d.Id == databaseId)
                               .AsEnumerable()
                               .FirstOrDefault();         
            if (_database == null)
            {
                _database = await _client.CreateDatabaseAsync(new Database { Id = databaseId });
            }
   
            _collection = _client.CreateDocumentCollectionQuery(_database.CollectionsLink)
                                 .Where(c => c.Id == collectionId)
                                 .AsEnumerable()
                                 .FirstOrDefault();           
            if (_collection == null)
            {
                _collection = await _client.CreateDocumentCollectionAsync(_database.CollectionsLink, new DocumentCollection { Id = collectionId } );
            }

            Console.WriteLine("Setup complete");
            return _collection;

        }

        public async Task<Database> TearDown()
        {
            var result = await _client.DeleteDatabaseAsync(_database.SelfLink);
            return result;
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }

        public async Task<long> Save(Patient patient)
        {
            var response = await _client.CreateDocumentAsync(_collection.DocumentsLink, patient);
            return response.DocumentUsage;
        }

        Database _database;
        DocumentCollection _collection;
        DocumentClient _client;
    }
}