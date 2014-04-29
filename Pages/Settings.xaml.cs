using MobileDeviceInfo.ViewModels;

namespace MobileDeviceInfo.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();
            DataContext = new SettingsAppearanceViewModel();
        }
    }
}
