namespace Api.Services
{
    using Application.Contracts;

    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService()
        {
            this.UserId = "11111111-a96e-4dc1-9cfa-4ac441fbeb6d";
        }

        public string UserId { get; }
    }
}
