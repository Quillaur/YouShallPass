using OverToolkit.Enums;
using OverToolkit.Helpers;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace You_Shall_Pass.Views
{
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
        }

        private async void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DeviceTypeHelper.GetDeviceFormFactorType() != DeviceFormFactorType.Phone)
                return;

            if (ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Portrait)
                await StatusBar.GetForCurrentView().ShowAsync();
            else
                await StatusBar.GetForCurrentView().HideAsync();
        }
    }
}
