using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;
using MobileDeviceInfo.Services;

namespace MobileDeviceInfo.ViewModels
{
    [Export]
    public class MainDeviceInfoViewModel : NotificationObject
    {
        private readonly MainInfoExtractor _mainInfoExtractor;
        private readonly DeviceInfoRetriever _deviceInfoRetriever;

        private Dictionary<string, string> _mainInfo;
        public Dictionary<string, string> MainInfo
        {
            get { return _mainInfo; }
            set
            {
                if (_mainInfo == value) return;
                _mainInfo = value;
                RaisePropertyChanged();
            }
        }

        public ICommand RetrieveInfoCommand { get; private set; }

        public MainDeviceInfoViewModel()
        {
            _deviceInfoRetriever = new DeviceInfoRetriever();
            _mainInfoExtractor = new MainInfoExtractor(_deviceInfoRetriever);
            
            RetrieveInfoCommand = new DelegateCommand(RetrieveInfoExecute);

            // try to retrieve info on startup
            RetrieveInfoExecute();
        }

        public void RetrieveInfoExecute()
        {
            var keys = _deviceInfoRetriever.GetDeviceInfoKeys();
            MainInfo = _mainInfoExtractor.ExtractMainInfo(keys);
        }
    }
}
