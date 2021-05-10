namespace Api.Services
{
    using Application.Contracts;

    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService()
        {
            this.UserId = "Anonymous";
        }

        public string UserId { get; }
    }
}
