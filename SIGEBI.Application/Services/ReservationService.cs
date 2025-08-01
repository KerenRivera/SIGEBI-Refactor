using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SIGEBI.Application.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public ReservationService(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            IBookRepository bookRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public IEnumerable<ReservationDto> GetAllReservations()
        {
            var reservations = _reservationRepository.GetAll();
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                BookId = r.BookId,
                UserName = r.User.Name,
                ReservationDate = r.ReservationDate,
                Status = r.Status
            });
        }

        public ReservationDto GetReservationById(int id)
        {
            var r = _reservationRepository.GetById(id);
            if (r == null) return null;

            return new ReservationDto
            {
                Id = r.Id,
                BookId = r.BookId,
                UserName = r.User.Name,
                ReservationDate = r.ReservationDate,
                Status = r.Status
            };
        }

        public void AddReservation(ReservationDto dto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Name == dto.UserName);
            var book = _bookRepository.GetById(dto.BookId);

            if (user == null || book == null)
                throw new System.Exception("The user or Book does not exists.");

            var reservation = new Reservation
            {
                BookId = dto.BookId,
                UserId = user.Id,
                ReservationDate = dto.ReservationDate,
                //set default status to true
                Status = true 
            };

            _reservationRepository.Add(reservation);
        }

        public void UpdateReservation(ReservationDto dto)
        {
            var reservation = _reservationRepository.GetById(dto.Id);
            if (reservation == null) return;

            reservation.Status = dto.Status;
            reservation.ReservationDate = dto.ReservationDate;
            _reservationRepository.Update(reservation);
        }

        public void DeleteReservation(int id)
        {
            _reservationRepository.Delete(id);
        }
    }
}
