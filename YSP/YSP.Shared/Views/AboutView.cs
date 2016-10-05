using Windows.UI.Xaml.Controls;

namespace YSP.Views
{
#if WINDOWS_APP
    public sealed partial class AboutView : SettingsFlyout
#else
    public sealed partial class AboutView : Page
#endif
    {
        public AboutView()
        {
            InitializeComponent();
        }
    }
}
