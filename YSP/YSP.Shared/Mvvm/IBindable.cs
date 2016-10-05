﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OverToolkit.Mvvm
{
    /// <summary>
    /// Расширение <see cref="INotifyPropertyChanged"/>, позволяющее вызывать вручную <see cref="INotifyPropertyChanged.PropertyChanged"/>
    /// </summary>
    internal interface IBindable : INotifyPropertyChanged
    {
        void RaisePropertyChanged([CallerMemberName]string propertyName = null);
    }
}
