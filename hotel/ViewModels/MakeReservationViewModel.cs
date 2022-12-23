using hotel.Commands;
using hotel.Models;
using hotel.Services;
using hotel.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hotel.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
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
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel hotel, NavigationService reservationViewNavigationSevice)
        {
            SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationSevice);
            CancelCommand = new NavigateCommand(reservationViewNavigationSevice);
        }
    }
}
