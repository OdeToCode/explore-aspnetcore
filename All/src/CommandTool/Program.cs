using CommandTool.DocumentDb;
using Microsoft.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandTool
{
    public class Program
    {
        public IConfiguration Configuration { get; set; }

        public void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder(".")
                                .AddJsonFile("config.json")
                                .AddEnvironmentVariables()
                                .Build();
            Console.WriteLine(Configuration.Get("DbUri"));
            try {
                var task = DocSerializtionExperiment();
                task.Wait();
            }
            catch(AggregateException ex)
            {
                foreach(var exception in ex.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                }
            }
        }

        private async Task<bool> DocSerializtionExperiment()
        {
            var store = new PatientStore(Configuration);
            await store.Setup();

            var patient = new Patient();
            patient.Name = "Scott";
            patient.Medications = null;

            await store.Save(patient);

            //await store.TearDown();
            return true;
        }

        private async Task<bool> DocSizeExperiment()
        {
            using (var store = new PatientStore(Configuration))
            {
                await store.Setup();
                for (var i = 0; i < 1; i++)
                {
                    var patient = new Patient();
                    patient.Birthdate = DateTime.Now;
                    patient.Id = i.ToString();
                    patient.Name = "Scott" + i.ToString();
                    patient.Procedures = new List<Procedure>();

                    for (var count = 0; count < 7000; count++)                    
                    {
                        var procedure = new Procedure { Code = GenerateCode(), Ordinal = count, Performed = DateTime.Now };
                        patient.Procedures.Add(procedure);
                    };
                    var collectionUsage = await store.Save(patient);
                    Console.WriteLine(collectionUsage);
                }
                await store.TearDown();
            }

            return true;
        }

        private string GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
