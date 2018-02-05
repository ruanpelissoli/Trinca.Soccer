using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Trinca.Soccer.App.Helpers
{
	public static class Settings
	{
	    private static ISettings AppSettings => CrossSettings.Current;

	    const string EmployeeIdKey = "employeeid";
	    static readonly int EmployeeIdDefault = 0;

	    const string EmployeeNameKey = "employeename";
	    static readonly string EmployeeNameDefault = string.Empty;

	    public static string EmployeeName
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(EmployeeNameKey, EmployeeNameDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(EmployeeNameKey, value);
	        }
	    }

	    public static int EmployeeId
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(EmployeeIdKey, EmployeeIdDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(EmployeeIdKey, value);
	        }
	    }

	    public static bool IsLoggedIn => EmployeeId > 0;

	    public static void Clear()
	    {
	        EmployeeName = null;
	        EmployeeId = 0;
	    }
    }
}