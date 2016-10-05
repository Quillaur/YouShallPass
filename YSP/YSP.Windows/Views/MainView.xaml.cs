using OverToolkit.Helpers;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Controls;

namespace YSP.Views
{
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
            SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;
        }

        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand(nameof(AboutView), LocalizationHelper.ToString("AboutFlyout"), (handler) =>
                new AboutView().Show()));
        }
    }
}
