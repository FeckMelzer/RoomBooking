using RoomBooking.Core.Contracts;
using RoomBooking.Core.Entities;
using RoomBooking.Persistence;
using RoomBooking.Wpf.Common;
using RoomBooking.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomBooking.Wpf.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
        public ObservableCollection<Booking> _booking;
        public ObservableCollection<Room> _room;
        public Booking selectedBooking;
        public Room selectedRoom;
        private Booking _selectedBookingNow;

        public ObservableCollection<Booking> Booking
        {
            get => _booking;
            set
            {
                _booking = value;
                OnPropertyChanged(nameof(Booking));
            }
        }

        public ObservableCollection<Room> Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        public Room SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
                LoadBookingsAsync();
            }
        }
        
        public Booking SelectedBooking
        {
            get => selectedBooking;
            set
            {
                selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }
        public MainViewModel(IWindowController windowController) : base(windowController)
        {
        }

        private async Task LoadDataAsync()
        {
            using IUnitOfWork uow = new UnitOfWork();
            var rooms = await uow.Rooms
              .GetAllAsync();
            Room = new ObservableCollection<Room>(rooms);
            selectedRoom = Room.First();
            await LoadBookingsAsync();


        }

            private async Task LoadBookingsAsync()
            {
                _selectedBookingNow = SelectedBooking;
            using IUnitOfWork uow = new UnitOfWork();

            var bookings = await uow.Bookings
                  .GetByRoomWithCustomerAsync(SelectedRoom.Id);
                Booking = new ObservableCollection<Booking>(bookings);

                if (_selectedBookingNow == null)
                {
                    SelectedBooking = Booking.First();

                }
                else
                {
                    SelectedBooking = _selectedBookingNow;

                }

            }
        

      

        public static async Task<MainViewModel> CreateAsync(IWindowController windowController)
        {
            var viewModel = new MainViewModel(windowController);
            await viewModel.LoadDataAsync();
            return viewModel;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
             throw new NotImplementedException();
        }

        private ICommand _cmdEditCustomer;
        public ICommand CmdEditCustomer
        {
            get
            {
                if (_cmdEditCustomer == null)
                {
                    _cmdEditCustomer = new RelayCommand(
                       execute: _ =>
                       {
                           Controller.ShowWindow(new EditCustomerViewModel(Controller, SelectedBooking.Customer), true);
                           LoadDataAsync();
                       },
                       canExecute: _ => SelectedBooking != null);

                }
                return _cmdEditCustomer;
            }

        }

    }
   
}
