using System.Data.Common;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using resevation_be.DTOs;
using resevation_be.models;

namespace resevation_be.Services
{
    public class ReservationService : IReservationService
    {
        private AppDbContext Db { get; }

        public ReservationService(AppDbContext _db)
        {
            Db = _db;
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string idToken)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(idToken);
                string uid = decodedToken.Uid;
                var users = Db.Users.Where(u => u.Uid == uid).Include(u => u.Reservations).ToList();
                if (users.Count > 0)
                {
                    return users[0].Reservations.Select(r => new ReservationDto()
                    {
                        Name = r.Name,
                        Date = r.ReservationDate
                    }).ToList();
                }
                return new List<ReservationDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ReseveAsync(ReservationDto reservation, string authentication)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(authentication);
                string uid = decodedToken.Uid;
                var user = Db.Users.First(u => u.Uid == uid);
                Db.Add(new Reservation()
                {
                    UserId = user.UserId,
                    Name = reservation.Name,
                    ReservationDate = reservation.Date
                });
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}