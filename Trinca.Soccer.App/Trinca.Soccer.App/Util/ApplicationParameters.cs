using PCLAppConfig;

namespace Trinca.Soccer.App.Util
{
    public static class ApplicationParameters
    {
        public static string ApiUrl => ConfigurationManager.AppSettings.Get("ApiUrl");
    }
}
