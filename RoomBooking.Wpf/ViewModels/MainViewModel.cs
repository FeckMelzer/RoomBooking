using RoomBooking.Core.Contracts;
using RoomBooking.Core.Entities;
using RoomBooking.Persistence;
using RoomBooking.Wpf.Common;
using RoomBooking.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RoomBooking.Wpf.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
        public ObservableCollection<Booking> _booking;
        /*public ObservableCollection<Room> _room;
        public ObservableCollection<Customer> _customer;*/
        public string _to;
        public string _from;

        public ObservableCollection<Booking> Booking
        {
            get => _booking;
            set
            {
                _booking = value;
                OnPropertyChanged(nameof(Booking));
            }
        }

        /*public ObservableCollection<Room> Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        public ObservableCollection<Customer> Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
        */
        public string From
        {
            get => _from;
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        public string To
        {
            get => _to;
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        public MainViewModel(IWindowController windowController) : base(windowController)
        {
            LoadDataAsync();
        }

        private  Task LoadDataAsync()
        {
            using IUnitOfWork uow = new UnitOfWork();
            var bookings =  uow.Bookings
               
              .GetAllAsync();



           // Booking = new ObservableCollection<Booking>(bookings);
            return bookings;

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

        private string _filterText = "";
        public string FilterText
        {
            get
            {
                return _filterText;
            }

            set
            {
                _filterText = value;
                LoadEmployees();

            }
        }
    }
}
