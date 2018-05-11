using Trinca.Soccer.App.Views;

namespace Trinca.Soccer.App.Constants
{
    public static class Routes
    {
        private const string NavigationPage = "NavigationPage";
        private const string CustomNavigationPage = nameof(Views.CustomNavigationPage);

        private const string Reset = "app:///";

        private static string IsReset(bool isReset)
        {
            return isReset ? Reset : string.Empty;
        }

        public static string Login(bool resetStack = false)
        {
            return $"{IsReset(resetStack)}{NavigationPage}/{nameof(LoginPage)}";
        }

        public static string Matches(bool resetStack = false)
        {
            return $"{IsReset(resetStack)}{NavigationPage}/{nameof(MainPage)}/{nameof(MatchesListPage)}";
        }

        public static string Match(int matchId)
        {
            return $"{nameof(MatchPage)}?{Parameters.MatchId}={matchId}";
        }

        public static string AddGuest()
        {
            return $"{nameof(AddGuestPage)}";
        }

        public static string Configuration()
        {
            return $"{NavigationPage}/{nameof(SettingsPage)}";
        }
    }
}
