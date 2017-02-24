using OverToolkit.Controls;
using Windows.UI.Xaml;

namespace You_Shall_Pass.Views
{
    public sealed partial class AboutView : View
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void TitleBar_BackButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
