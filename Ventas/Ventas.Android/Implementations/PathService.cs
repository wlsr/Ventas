[assembly: Xamarin.Forms.Dependency(typeof(Ventas.Droid.Implementations.PathService))]
namespace Ventas.Droid.Implementations
{
    using Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Ventas.db3");
        }
    }
}