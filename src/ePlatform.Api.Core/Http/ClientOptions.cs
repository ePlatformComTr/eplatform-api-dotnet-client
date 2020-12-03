namespace ePlatform.Api.Core.Http
{
    public class ClientOptions
    {
        public string AuthServiceUrl { get; set; }
        public string InvoiceServiceUrl { get; set; }
        public string TicketServiceUrl { get; set; }
        public AuthOption Auth { get; set; }

        public ClientOptions()
        {
            Auth = new AuthOption();
        }

        public class AuthOption
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ClientId { get; set; } = "serviceApi";
        }
    }
}
