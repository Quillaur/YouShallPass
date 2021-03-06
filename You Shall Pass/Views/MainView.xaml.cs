﻿using OverToolkit.Controls;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace You_Shall_Pass.Views
{
    public sealed partial class MainView : View
    {
        public MainView()
        {
            InitializeComponent();
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
    }
}
