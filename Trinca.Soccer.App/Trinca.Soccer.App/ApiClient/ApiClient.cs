using Refit;
using Trinca.Soccer.App.Util;

namespace Trinca.Soccer.App.ApiClient
{
    public static class ApiClient
    {
        private static readonly string ApiUrl = ApplicationParameters.ApiUrl;

        public static IEmployeesApi Employees { get; private set; }

        public static void Initialize()
        {
            Employees = RestService.For<IEmployeesApi>(ApiUrl);
        }
    }
}
