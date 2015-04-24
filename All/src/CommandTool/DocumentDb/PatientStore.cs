using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using CommandTool.DocumentDb;
using Newtonsoft.Json;

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
            var db = new Database() { Id = "PatientDb" };
            var collection = new DocumentCollection { Id = "Patients" };

            _database = await _client.ReadDatabaseAsync(db.SelfLink);
            if (_database == null)
            {
                _database = await _client.CreateDatabaseAsync(new Database { Id = "PatientDb" });
            }
            _collection = await _client.ReadDocumentCollectionAsync(_database.CollectionsLink);
            if (_collection == null)
            {
                _collection = await _client.CreateDocumentCollectionAsync(_database.CollectionsLink, collection);
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