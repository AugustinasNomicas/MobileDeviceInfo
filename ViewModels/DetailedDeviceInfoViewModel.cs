using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;
using MobileDeviceInfo.Services;

namespace MobileDeviceInfo.ViewModels
{
    [Export]
    public class DetailedDeviceInfoViewModel : NotificationObject
    {
        private readonly DeviceInfoRetriever _deviceInfoRetriever;

        private Dictionary<string, string> _detailedInfo;
        public Dictionary<string, string> DetailedInfo
        {
            get { return _detailedInfo; }
            set
            {
                if (_detailedInfo == value) return;
                _detailedInfo = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsWaitingForInfo");
            }
        }

        public ICommand RetrieveInfoCommand { get; private set; }

        public bool IsWaitingForInfo
        {
            get
            {
                return _detailedInfo == null;
            }
        }

        public DetailedDeviceInfoViewModel()
        {
            _deviceInfoRetriever = new DeviceInfoRetriever();
            
            RetrieveInfoCommand = new DelegateCommand(RetrieveInfoExecute);

            // try to retrieve info on startup
            RetrieveInfoExecute();
        }

        public async void RetrieveInfoExecute()
        {
            DetailedInfo = null; // reset to null for loading animation (if user presses refresh button)
            var keys = _deviceInfoRetriever.GetDeviceInfoKeys();
            DetailedInfo =
                await Task.Run(() => keys.ToDictionary(k => k, v => _deviceInfoRetriever.GetDeviceInfoValue(v)));
        }

        
    }
}
