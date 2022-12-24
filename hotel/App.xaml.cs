using hotel.DbContexts;
using hotel.Exceptions;
using hotel.Models;
using hotel.Services;
using hotel.Services.ReservationConflictValidators;
using hotel.Services.ReservationCreators;
using hotel.Services.ReservationProviders;
using hotel.Stores;
using hotel.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace hotel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=reservroom.db";
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private readonly HotelStore _hotelStore;
        private ReservroomDbContextFactory _reservroomDbContextFactory;

        public App()
        {
            _reservroomDbContextFactory = new ReservroomDbContextFactory(CONNECTION_STRING);
            IReservationProvider reservationProvider = new DataBaseReservationProvider(_reservroomDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservroomDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservroomDbContextFactory);


            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
            _hotel = new Hotel("NikitaHotel", reservationBook);
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (ReservroomDbContext dbContext = _reservroomDbContextFactory.CreateDbContext()) { dbContext.Database.Migrate(); }


            _navigationStore.CurrentViewModel = CreateReservationViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
        }

        private ReservationListingViewModel CreateReservationViewModel()
        {
            return  ReservationListingViewModel.LoadViewModel(_hotelStore, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }
}
