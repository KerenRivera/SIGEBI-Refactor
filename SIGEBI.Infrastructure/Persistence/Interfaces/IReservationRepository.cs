using SIGEBI.Domain.Entities;
using System.Collections.Generic;

namespace SIGEBI.Infrastructure.Interfaces
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int id);
    }
}
