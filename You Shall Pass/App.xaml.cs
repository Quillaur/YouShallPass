using OverToolkit.Enums;
using OverToolkit.Helpers;
using Windows.ApplicationModel.Activation;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using You_Shall_Pass.Views;

namespace You_Shall_Pass
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private Frame frame;

        /// <summary>
        /// Initializes the singleton application object.
        /// This is the first line of authored code executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.
        /// Other entry points will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            frame = Window.Current.Content as Frame;

            if (frame != null)
                return;
            frame = new Frame();
            Window.Current.Content = frame;
            frame.Navigate(typeof(MainView));

            frame.Navigated += OnNavigated;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
                HardwareButtons.BackPressed += OnBackPressed;

            if (!e.PrelaunchActivated)
                Window.Current.Activate();

            UpdateBackButtonVisibility();
        }

        private void OnBackPressed(object sender, BackPressedEventArgs e)
        {
            if (!frame.CanGoBack)
                return;
            e.Handled = true;
            frame.GoBack();
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!frame.CanGoBack)
                return;
            e.Handled = true;
            frame.GoBack();
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            UpdateBackButtonVisibility();
        }

        private void UpdateBackButtonVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = frame.CanGoBack ? AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }
    }
}
