using System;
using System.Threading.Tasks;
using consumer.event_processor;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Azure.EventHubs.Processor;
using common.model;

namespace consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine($"Register the {nameof(user_activity_event_processor)}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var eventProcessorHost = new EventProcessorHost(
              configuration.GetSection("EventHub").GetSection("UserActivity").GetSection("Path").Value,
              configuration.GetSection("EventHub").GetSection("UserActivity").GetSection("ConsumerGroupName").Value,
              configuration.GetSection("EventHub").GetSection("UserActivity").GetSection("ConnectionString").Value,
              configuration.GetSection("EventHub").GetSection("UserActivity").GetSection("StorageConnectionString").Value,
              configuration.GetSection("EventHub").GetSection("UserActivity").GetSection("BlobContainerName").Value);

            await eventProcessorHost.RegisterEventProcessorAsync<user_activity_event_processor>();

            Console.WriteLine("Waiting for incoming events...");
            Console.WriteLine("Press any key to shutdown");
            Console.ReadLine();

            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
