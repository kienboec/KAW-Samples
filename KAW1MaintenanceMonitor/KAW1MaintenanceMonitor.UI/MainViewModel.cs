using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace KAW1MaintenanceMonitor.UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string EnteredMaintenanceMessage { get; set; }

        public string CurrentMaintenanceMessage { get; set; }

        public RelayCommand SetCommand { get; }
        public RelayCommand ResetCommand { get; }
        public RelayCommand CheckCommand { get; }

        public MainViewModel()
        {
            SetCommand = new RelayCommand(SetAction);
            ResetCommand = new RelayCommand(ResetAction);
            CheckCommand = new RelayCommand(CheckAction);
        }

        public void SetAction()
        {

        }

        public void ResetAction()
        {

        }

        public async void CheckAction()
        {
            HttpClient client = new HttpClient();
            CurrentMaintenanceMessage = await client.GetStringAsync("https://localhost:44328/api/Maintenance");
            OnPropertyChanged(nameof(CurrentMaintenanceMessage));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
