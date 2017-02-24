using Microsoft.Services.Store.Engagement;
using OverToolkit.Helpers;
using OverToolkit.Mvvm;
using System;
using System.Globalization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;

namespace You_Shall_Pass.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        #region Constructor
        public AboutViewModel()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }
        #endregion

        #region Commands
        public DelegateCommand SendMailCommand => new DelegateCommand(async () =>
        {
            PackageVersion version = Package.Current.Id.Version;
            string mailto = $"mailto:";

            await Launcher.LaunchUriAsync(new Uri($"{mailto}quillaur@outlook.com?subject=You Shall Pass {version.Major}.{version.Minor}.{version.Build}"));
        });

        public DelegateCommand RateCommand => new DelegateCommand(async () =>
        {
            await Launcher.LaunchUriAsync(new Uri(@"ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName));
        });

        public DelegateCommand ShareLinkCommand => new DelegateCommand(() =>
        {
            DataTransferManager.ShowShareUI();
        });

        public DelegateCommand GoToFeedbackHubCommand => new DelegateCommand(async () =>
        {
            await StoreServicesFeedbackLauncher.GetDefault().LaunchAsync();
        });
        #endregion

        #region Properties
        public bool IsFeedbackHyperlinkVisible => StoreServicesFeedbackLauncher.IsSupported();

        public string Version
        {
            get
            {
                PackageVersion version = Package.Current.Id.Version;
                return $"{LocalizationHelper.ToString("Version")} {version.Major}.{version.Minor}.{version.Build}";
            }
        }

        public string LastUpdateDate => new DateTime(2017, 2, 25).ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
        #endregion

        #region Private methods
        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetWebLink(new Uri($"https://www.microsoft.com/en-us/store/p/you-shall-pass/9wzdncrd8dzj"));
            request.Data.Properties.Title = "You Shall Pass";
            request.Data.Properties.Description = LocalizationHelper.ToString("ShareDescription");
        }
        #endregion
    }
}
