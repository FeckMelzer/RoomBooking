using RoomBooking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace RoomBooking.ImportConsole
{
  public static class ImportController
  {
        /// <summary>
        /// Liest die Buchungen mit ihren Räumen und Kunden aus der
        /// csv-Datei ein.
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Booking>> ReadBookingsFromCsvAsync()
        {
            string[][] matrix = await MyFile.ReadStringMatrixFromCsvAsync("bookings.csv", true);
            var customers = matrix
              
                .Select(cust => new Customer
                {
                    FirstName = cust[1],
                    LastName = cust[0],
                    Iban = cust[2]

                }).GroupBy(line => line.FirstName + line.LastName + line.Iban).Select(s=> s.First()).ToArray();

            var rooms = matrix
                .GroupBy(line => line[3])
                .Select(ro => new Room
                {
                    RoomNumber = ro.Key
                }).ToArray();

            var bookings = matrix
                .Select(book => new Booking
                {
                    Customer = customers.Single(s => book[0] == s.LastName && book[1] == s.FirstName && book[2] == s.Iban),
                    Room = rooms.Single(s => book[3] == s.RoomNumber),
                    From = book[4],
                    To = book[5]
                }).ToArray();
            return bookings;
    }
  }
}
