using DemoAjax.Models;
using System.Collections.Generic;

namespace DemoAjax.Repository1
{
    public interface IRepository
    {
        IEnumerable<Reservation> Reservations { get; }

        Reservation this[int id] { get; }

        Reservation AddReservation(Reservation reservation);

        Reservation UpdateReservation(Reservation reservation);
        void DeleteReservation(int id);

    }
}
