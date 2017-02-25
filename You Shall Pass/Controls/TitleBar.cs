using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace You_Shall_Pass.Controls
{
    public sealed class TitleBar : Control
    {
        private bool isWindowDeactivated;
        private BackButton backButton;
        private CoreApplicationViewTitleBar coreTitleBar;
        private Grid leftMask, placeholder, rightMask;
        private Rectangle backgroundElement;
        private Grid title;

        public TitleBar()
        {
            DefaultStyleKey = typeof(TitleBar);
        }

        public event RoutedEventHandler BackButtonClick;

        private void OnBackButtonClicked()
        {
            RoutedEventHandler eh = BackButtonClick;
            eh?.Invoke(this, new RoutedEventArgs());
        }

        public bool IsBackButtonVisible
        {
            get { return (bool)GetValue(IsBackButtonVisibleProperty); }
            set {  SetValue(IsBackButtonVisibleProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value.ToUpper()); }
        }

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public bool ExtendIntoTitleBar
        {
            get { return (bool)GetValue(ExtendIntoTitleBarProperty); }
            set { SetValue(ExtendIntoTitleBarProperty, value); }
        }

        public bool IsTitleBarTabletModeVisible
        {
            get { return (bool)GetValue(IsTitleBarTabletModeVisibleProperty); }
            set { SetValue(IsTitleBarTabletModeVisibleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string),
                typeof(TitleBar), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string),
                typeof(TitleBar), new PropertyMetadata(null));

        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register(nameof(Glyph), typeof(string),
                typeof(TitleBar), new PropertyMetadata(null));

        public static readonly DependencyProperty ExtendIntoTitleBarProperty =
            DependencyProperty.Register(nameof(ExtendIntoTitleBar), typeof(bool),
                typeof(TitleBar), new PropertyMetadata(true, OnExtendIntoTitleBarChanged));

        public static readonly DependencyProperty IsTitleBarTabletModeVisibleProperty =
            DependencyProperty.Register(nameof(IsTitleBarTabletModeVisible), typeof(bool),
                typeof(TitleBar), new PropertyMetadata(true, null));

        public static readonly DependencyProperty IsBackButtonVisibleProperty =
            DependencyProperty.Register(nameof(IsBackButtonVisible), typeof(bool),
                typeof(TitleBar), new PropertyMetadata(true, OnBackButtonVisibilityChanged));

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            backButton = GetTemplateChild(nameof(backButton)) as BackButton;
            backgroundElement = GetTemplateChild(nameof(backgroundElement)) as Rectangle;
            leftMask = GetTemplateChild(nameof(leftMask)) as Grid;
            placeholder = GetTemplateChild(nameof(placeholder)) as Grid;
            rightMask = GetTemplateChild(nameof(rightMask)) as Grid;
            title = GetTemplateChild(nameof(title)) as Grid;

            if (!DesignMode.DesignModeEnabled)
            {
                Window.Current.SetTitleBar(backgroundElement);

                UpdateExtendIntoTitleBar();
                UpdateBackButtonVisibility();

                if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                {
                    ExtendIntoTitleBar = false;
                    title.Visibility = backButton.Visibility = leftMask.Visibility = rightMask.Visibility = Visibility.Collapsed;
                }

                backButton.Click += (sender, e) => OnBackButtonClicked();

                coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
                coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

                Window.Current.Activated += Window_Activated;
            }
        }

        private static void OnExtendIntoTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var titleBar = d as TitleBar;
            titleBar?.UpdateExtendIntoTitleBar();
        }

        private static void OnBackButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var titleBar = d as TitleBar;
            titleBar?.UpdateBackButtonVisibility();
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (IsTitleBarTabletModeVisible)
                return;

            if (sender.IsVisible)
            {
                title.Visibility = backButton.Visibility = leftMask.Visibility = rightMask.Visibility = Visibility.Collapsed;
            }
            else
            {
                title.Visibility = backButton.Visibility = leftMask.Visibility = rightMask.Visibility = Visibility.Visible;
            }
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            backButton.Height = sender.Height;
            title.Height = sender.Height;

            leftMask.Width = sender.SystemOverlayLeftInset;
            rightMask.Width = sender.SystemOverlayRightInset;
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs e)
        {
            isWindowDeactivated = e.WindowActivationState == CoreWindowActivationState.Deactivated;
            title.Opacity = backButton.Opacity = e.WindowActivationState != CoreWindowActivationState.Deactivated ? 1 : 0.5;
        }

        private void UpdateExtendIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = ExtendIntoTitleBar;
        }

        private void UpdateBackButtonVisibility()
        {
            if (backButton != null)
                backButton.Visibility = IsBackButtonVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
