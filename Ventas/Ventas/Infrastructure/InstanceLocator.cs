
namespace Ventas.Infrastructure
{
    using Ventas.ViewModels;

    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }

    }
}
