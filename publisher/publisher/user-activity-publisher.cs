using System.Text;
using System.Threading.Tasks;
using common.model;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace publisher
{
    public class user_activity_publisher
    {
        private EventHubClient _eventHubClient;
        public user_activity_publisher(string eventHubConnectionString)
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }

        public async Task PublishEventAsync(user_activity data)
        {
            await _eventHubClient.SendAsync(CreateEventData(data));
        }

        private static EventData CreateEventData(user_activity data)
        {
            return new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }
    }
}