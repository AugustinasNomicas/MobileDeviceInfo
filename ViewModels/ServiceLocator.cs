using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace MobileDeviceInfo.ViewModels
{
    public class ServiceLocator
    {
        private static AssemblyCatalog _catalog;
        private static CompositionContainer _container;

        public ServiceLocator()
        {
            if (_catalog == null || _container == null)
            {
                _catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                _container = new CompositionContainer(_catalog);
                _container.ComposeParts();
            }

            _container.SatisfyImportsOnce(this);
        }

        [Import]
        public MainDeviceInfoViewModel MainDeviceInfoViewModelInstance { get; set; }

        [Import]
        public DetailedDeviceInfoViewModel DetailedDeviceInfoViewModelInstance { get; set; }
    }
}
