using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using CommandTool.DocumentDb;

namespace CommandTool
{
    public class PatientStore : IDisposable
    {
        public PatientStore(IConfiguration configuration)
        {
            _client = new DocumentClient(
                new Uri(configuration.Get("DbUri")), 
                configuration.Get("DbToken"));                
        }
        
        public async Task<DocumentCollection> Setup()
        {
            _database = await _client.CreateDatabaseAsync(new Database { Id = "PatientDb" });
            _collection = await _client.CreateDocumentCollectionAsync(_database.CollectionsLink,
                                                new DocumentCollection { Id = "Patients" });
            return _collection;
        }

        public async Task<Database> TearDown()
        {
            var result = await _client.DeleteDatabaseAsync(_database.SelfLink);
            return result;
        }

        public void Dispose()
        {
            if(_client != null)
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