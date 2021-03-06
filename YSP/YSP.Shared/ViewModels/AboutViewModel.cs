﻿using OverToolkit.Helpers;
using OverToolkit.Mvvm;
using System;
using System.Globalization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.System;
using YSP.Enums;

namespace YSP.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        #region Constructor
        public AboutViewModel() { }
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
#if WINDOWS_APP
            await Launcher.LaunchUriAsync(new Uri($@"ms-windows-store:REVIEW?PFN={Package.Current.Id}"));
#else
            await Launcher.LaunchUriAsync(new Uri($@"ms-windows-store:reviewapp?appid={CurrentApp.AppId}"));
#endif
        });

        public DelegateCommand ShareLinkCommand => new DelegateCommand(() =>
        {
            DataTransferManager.ShowShareUI();
        });
#endregion

#region Properties
        public string Version
        {
            get
            {
                DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
                dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(DataTransferManager_DataRequested);

                PackageVersion version = Package.Current.Id.Version;
                return $"{LocalizationHelper.ToString("Version")} {version.Major}.{version.Minor}.{version.Build}";
            }
        }

        public string LastUpdateDate => new DateTime(2016, 10, 8).ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);

        public string Mail => LocalizationHelper.ToString(nameof(Mail), TextTransform.Lowercase);

        public string Website => LocalizationHelper.ToString(nameof(Website), TextTransform.Lowercase);

        public string RateAndReview => LocalizationHelper.ToString(nameof(RateAndReview), TextTransform.Lowercase);

        public string Share => LocalizationHelper.ToString(nameof(Share), TextTransform.Lowercase);

        public string SourceCodeOnGitHub => LocalizationHelper.ToString(nameof(SourceCodeOnGitHub), TextTransform.Lowercase);
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
