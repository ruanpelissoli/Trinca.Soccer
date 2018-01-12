using Trinca.Soccer.App.Views;

namespace Trinca.Soccer.App.Constants
{
    public static class Routes
    {
        public static string Login()
        {
            return $"NavigationPage/{nameof(LoginPage)}";
        }

        public static string Matches()
        {
            return $"NavigationPage/{nameof(MatchesPage)}/{nameof(MatchesListPage)}";
        }
    }
}
