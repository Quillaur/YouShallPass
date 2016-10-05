using Windows.ApplicationModel.Activation;
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
#else
using Windows.Phone.UI.Input;
#endif
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#if WINDOWS_PHONE_APP
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
#endif
using YSP.Views;

namespace YSP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private Frame frame;
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

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
        /// Other entry points will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            frame = Window.Current.Content as Frame;

            if (frame != null)
                return;
            frame = new Frame();
            Window.Current.Content = frame;

#if WINDOWS_PHONE_APP
            if (frame.ContentTransitions != null)
            {
                transitions = new TransitionCollection();
                foreach (Transition c in frame.ContentTransitions)
                    transitions.Add(c);
            }

            frame.ContentTransitions = null;
            frame.Navigated += RootFrame_FirstNavigated;

            HardwareButtons.BackPressed += OnBackPressed;
#endif
            frame.Navigate(typeof(MainView));
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            frame = sender as Frame;
            frame.ContentTransitions = transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            frame.Navigated -= RootFrame_FirstNavigated;
        }

        private void OnBackPressed(object sender, BackPressedEventArgs e)
        {
            if (!frame.CanGoBack)
                return;
            e.Handled = true;
            frame.GoBack();
        }
#endif
    }
}
