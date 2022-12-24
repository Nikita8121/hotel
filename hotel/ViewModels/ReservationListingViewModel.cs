using hotel.Commands;
using hotel.Models;
using hotel.Services;
using hotel.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hotel.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly HotelStore _hotelStore;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand MakeReservationCommand { get; }
        public ICommand LoadReservationsCommand { get; }
        private bool _isLoading = false;
        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);


        public ReservationListingViewModel(HotelStore hotelStore,NavigationService makeReservationNavigationService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();


            LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);    
            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

            _hotelStore = hotelStore;
            _hotelStore.ReservationMade += OnReservationMade;
        }

        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);

            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
