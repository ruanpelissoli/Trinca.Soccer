using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Trinca.Soccer.App.Helpers
{
	public static class Settings
	{
	    private static ISettings AppSettings => CrossSettings.Current;

	    const string UserIdKey = "userid";
	    static readonly int UserIdDefault = 0;

	    const string AuthTokenKey = "authtoken";
	    static readonly string AuthTokenDefault = string.Empty;

	    public static string AuthToken
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(AuthTokenKey, AuthTokenDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(AuthTokenKey, value);
	        }
	    }

	    public static int UserId
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(UserIdKey, value);
	        }
	    }

	    public static bool IsLoggedIn => UserId > 0;

	    public static void Clear()
	    {
	        AuthToken = null;
	        UserId = 0;
	    }
    }
}