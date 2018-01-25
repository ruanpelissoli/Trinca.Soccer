﻿using PCLStorage;
using Plugin.EmbeddedResource;
using System.Reflection;
using System.Threading.Tasks;

namespace Trinca.Soccer.App.Helpers
{
    public static class EmbeddedResourceManager
    {
        public static async Task Initialize(Assembly assembly)
        {
            var rootFolder = FileSystem.Current.LocalStorage;

            var writer = new ResourceWriter(assembly);

            // Only need to write the bundled files once.
            if (await rootFolder.CheckExistsAsync("arrow-left.png") == ExistenceCheckResult.NotFound)
            {
                await writer.WriteFile("Images/arrow-left.png", rootFolder.Path);
                await writer.WriteFile("Images/arrow-right.png", rootFolder.Path);
                await writer.WriteFile("Images/remove.png", rootFolder.Path);
                await writer.WriteFolder("Images", rootFolder.Path);
            }
        }
    }
}