using PCLStorage;
using Plugin.EmbeddedResource;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Trinca.Soccer.App.Helpers
{
    public static class EmbeddedResourceManager
    {
        public static async Task Initialize(Assembly assembly)
        {
            try
            {
                var rootFolder = FileSystem.Current.LocalStorage;

                var writer = new ResourceWriter(assembly);

                if (await rootFolder.CheckExistsAsync("logo.png") == ExistenceCheckResult.NotFound)
                {
                    await writer.WriteFile("Images/arrow-left.png", rootFolder.Path);
                    await writer.WriteFile("Images/arrow-right.png", rootFolder.Path);
                    await writer.WriteFile("Images/remove.png", rootFolder.Path);
                    await writer.WriteFile("Images/logo.png", rootFolder.Path);
                    await writer.WriteFolder("Images", rootFolder.Path);
                }
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            
        }
    }
}
