using hotel.Commands;
using hotel.Models;
using hotel.Services;
using hotel.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hotel.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string _username;
        private int _floorNumber;
        private int _roomNumber;
        private DateTime _startDate = new DateTime(2022, 1, 1);
        private DateTime _endDate = new DateTime(2022, 1, 8);

        public string Username
        {
            get { return _username; }
            set 
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public int FloorNumber
        {
            get { return _floorNumber; }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set 
            { 
                _startDate = value; 
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The start date cannot be after the end date", nameof(StartDate));
                }
                
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set 
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The end date cannot be before the start date", nameof(EndDate));            
                }

                
            }
        }


        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

  

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationSevice)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationSevice);
            CancelCommand = new NavigateCommand(reservationViewNavigationSevice);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
        private void AddError(string errorMessage, string propertyName)
        {
            if(!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }
    }
}
