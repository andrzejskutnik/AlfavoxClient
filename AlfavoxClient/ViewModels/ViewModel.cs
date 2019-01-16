using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace AlfavoxClient
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Binding properties

        private IDictionary<int, string> _items;
        public IDictionary<int, string> Items { get => _items; set => SetProperty(ref _items, value); }

        private string _keys;
        public string Keys { get => _keys; set => SetProperty(ref _keys, value); }

        private string _status ="Waiting for user action";
        public string Status { get => _status; set => SetProperty(ref _status, value); }

        #endregion Binding properties

        #region Cmd methods

        internal void FindKeysCmd(object sender, ExecutedRoutedEventArgs e)
        {
            if (_keys == null) return;

            var keys = _keys.ParseModel();
            if (keys == null || keys.Count() < 1)
            {
                Status = "Invalid key";
                return;
            }
            try
            {
                if (keys.Count() == 1)
                    Items = Service.GetDataAsync(keys.First()).Result;

                if (keys.Count() > 1)
                    Items = Service.GetDataAsync(_keys).Result;
            }
            catch (System.Exception)
            {
                // todo: handle services exception.
                // todo: add textbox error parent.
                Status = "Service error!!";
                return;
            }
            Status = " Ready";
        }

        internal void CanCloseCmd(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        internal void CloseCmd(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is Window w) w.Close();
        }

        internal void CanFindKeysCmd(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        #endregion Cmd methods

        #region BaseMVVM

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Method to events

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        #endregion Method to events

        #region Protection methods

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

            return true;
        }

        #endregion Protection methods

        #endregion BaseMVVM
    }
}