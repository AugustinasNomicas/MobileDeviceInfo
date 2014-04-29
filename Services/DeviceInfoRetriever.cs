using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using RegawMOD.Android;

namespace MobileDeviceInfo.Services
{
    public class DeviceInfoRetriever : IDisposable
    {
        private readonly AndroidController _android;
        private string _deviceSerial;
        private Device _device;

        private bool _disposed;

        public DeviceInfoRetriever()
        {
            _android = AndroidController.Instance;
        }

        public async Task<List<string>> GetDeviceInfoKeysAsync()
        {
            var task = Task.Run(() => GetDeviceInfoKeys());

            var result = await task;

            return result;
        }

        private void ConnectToDevice()
        {
            _android.UpdateDeviceList();

            if (!_android.HasConnectedDevices) return;
            _deviceSerial = _android.ConnectedDevices[0];
            _device = _android.GetConnectedDevice(_deviceSerial);
        }

        public List<string> GetDeviceInfoKeys()
        {
            if (_device == null)
                ConnectToDevice();

            return _device != null ? _device.BuildProp.Keys : new List<string>();
        }

        public string GetDeviceInfoValue(string key)
        {
            Debug.WriteLine("Getting key value: " + key);
            return _device.BuildProp.GetProp(key);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _android.Dispose();
            }
            _disposed = true;
        }
    }
}