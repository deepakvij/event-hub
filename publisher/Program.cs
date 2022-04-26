using System.Security.Cryptography;
using System;
using System.IO;
using System.Threading.Tasks;
using common.model;
using Microsoft.Extensions.Configuration;

namespace publisher
{
    public class Program
    {
        private static Random _randomGenerator = new Random();

        public static void Main(string[] args)
        {
            MainAsync(args).Wait();

            Console.Write("Hit enter to stop the publisher...");
            Console.ReadLine();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine($"Starting the publisher: {nameof(user_activity_publisher)}");

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            while (true)
            {
                var userActivityPublisher = new user_activity_publisher(configuration.GetSection("EventHub").GetSection("UserActivity").GetSection("ConnectionString").Value);
                await userActivityPublisher.PublishEventAsync(GetFakeUserActivitityData());
            }
        }

        private static user_activity GetFakeUserActivitityData()
        {
            var randomNumber = _randomGenerator.Next(10000000);
            var randomApiNumber = _randomGenerator.Next(10);
            var randomOtherNumber = _randomGenerator.Next(100);
            var guid = Guid.NewGuid();

            return new user_activity()
            {
                // Id = guid.ToString(),
                ServerHostName = Environment.MachineName,
                RequestContentType = "application/json",
                RequestUri = $"http://services.mycompany.com/api/v1/{randomApiNumber}",
                RequestMethod = (randomOtherNumber % 3 == 0 ? "GET" : "POST"),
                RequestTimestamp = DateTime.Now,
                ResponseStatusCode = (randomOtherNumber % 5 == 0 ? "BadRequest" : "OK"),
                ResponseTimestamp = DateTime.Now.AddMilliseconds(randomOtherNumber),
                ApiName = $"Get{randomApiNumber}",
                CorrelationId = guid,
                UserId = 148,
                Vertical = (randomOtherNumber % 9 == 0 ? "Servicing" : "Origination")
            };
        }
    }
}
