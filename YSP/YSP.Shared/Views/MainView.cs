#if WINDOWS_APP
using OverToolkit.Helpers;
using Windows.UI.ApplicationSettings;
#endif
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace YSP.Views
{
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
#if WINDOWS_APP
            SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;
#endif
        }

        private void websiteBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                passwordBox.Focus(FocusState.Programmatic);
                e.Handled = true;
            }
        }

        private void passwordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                websiteBox.Focus(FocusState.Programmatic);
                e.Handled = true;
            }
        }

#if WINDOWS_APP
        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand(nameof(AboutView), LocalizationHelper.ToString("AboutFlyout"), (handler) =>
                new AboutView().Show()));
        }
#endif
    }
}
