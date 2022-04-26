namespace common.model
{
    public class user_activity
    {
        public string ServerHostName { get; set; } // ServerHostName (length: 255)
        public string RequestContentType { get; set; } // RequestContentType (length: 100)
        public string RequestUri { get; set; } // RequestUri (length: 2000)
        public string RequestMethod { get; set; } // RequestMethod (length: 10)
        public System.DateTime RequestTimestamp { get; set; } // RequestTimestamp
        public string ResponseStatusCode { get; set; } // ResponseStatusCode (length: 100)
        public System.DateTime? ResponseTimestamp { get; set; } // ResponseTimestamp
        public string ApiName { get; set; } // ApiName (length: 255)
        public System.Guid CorrelationId { get; set; } // CorrelationId
        public long UserId { get; set; } // UserId
        public string Vertical { get; set; } // Vertical (length: 100)
    }
}