using Trinca.Soccer.App.Views;

namespace Trinca.Soccer.App.Constants
{
    public static class Routes
    {
        private const string NavigationPage = nameof(CustomNavigationPage);

        public static string Login()
        {
            return $"{NavigationPage}/{nameof(LoginPage)}";
        }

        public static string Matches()
        {
            return $"{NavigationPage}/{nameof(MainPage)}/{nameof(MatchesListPage)}";
        }

        public static string Match(int matchId)
        {
            return $"{nameof(MatchPage)}?{Parameters.MatchId}={matchId}";
        }

        public static string AddGuest()
        {
            return $"{nameof(AddGuestPage)}";
        }
    }
}
