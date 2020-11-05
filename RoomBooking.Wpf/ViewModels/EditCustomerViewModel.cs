using RoomBooking.Core.Entities;
using RoomBooking.Wpf.Common;
using RoomBooking.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoomBooking.Wpf.ViewModels
{
    public class EditCustomerViewModel :BaseViewModel
    {
        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public EditCustomerViewModel(IWindowController controller, Customer customer) : base(controller)
        {
            Customer = customer;
        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }



    }
}
