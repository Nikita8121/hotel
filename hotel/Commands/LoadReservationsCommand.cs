using hotel.Models;
using hotel.Stores;
using hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace hotel.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
        }


        public override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;
            
            try
            {
                throw new Exception();
                await _hotelStore.Load();

                _viewModel.UpdateReservations(_hotelStore.Reservations);
            } catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations";
            }
            
            _viewModel.IsLoading = false;
        }
    }
}
