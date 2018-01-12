using Refit;
using Trinca.Soccer.App.Util;

namespace Trinca.Soccer.App.API
{
    public static class ClientApi
    {
        private static readonly string ApiUrl = ApplicationParameters.ApiUrl;

        public static IEmployeesApi Employees { get; private set; }
        public static IMatchesApi Matches { get; private set; }

        public static void Initialize()
        {
            Employees = RestService.For<IEmployeesApi>(ApiUrl);
            Matches = RestService.For<IMatchesApi>(ApiUrl);
        }
    }
}
