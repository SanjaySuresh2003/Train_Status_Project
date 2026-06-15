using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Status_API.Data;
using Train_Status_API.DTOs;

namespace Train_Status_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly TrainBookingDbContext _context;

        public BookingsController(TrainBookingDbContext context)
        {
            _context = context;
        }

        
        // FEATURE 3: PNR search — booking + train + passenger details
        
        [HttpGet("{pnr}")]
        public async Task<ActionResult<BookingDetailsDto>> GetByPnr(string pnr)
        {
            var booking = await _context.Bookings
                .Include(b => b.Train)
                .Include(b => b.SourceStation)
                .Include(b => b.DestinationStation)
                .Include(b => b.Class)
                .Include(b => b.Passengers)
                .FirstOrDefaultAsync(b => b.Pnr == pnr);

            if (booking == null)
                return NotFound(new { message = $"No booking found with PNR '{pnr}'" });

            var dto = new BookingDetailsDto
            {
                Pnr = booking.Pnr,
                TrainNumber = booking.Train.TrainNumber,
                TrainName = booking.Train.TrainName,
                JourneyDate = booking.JourneyDate,
                SourceStationName = booking.SourceStation.StationName,
                DestinationStationName = booking.DestinationStation.StationName,
                ClassCode = booking.Class.ClassCode,
                ClassName = booking.Class.ClassName,
                BookingStatus = booking.BookingStatus,
                BookingDate = booking.BookingDate,
                Passengers = booking.Passengers.Select(p => new PassengerDto
                {
                    PassengerName = p.PassengerName,
                    Age = p.Age,
                    Gender = p.Gender,
                    SeatNumber = p.SeatNumber,
                    CoachNumber = p.CoachNumber,
                    PassengerStatus = p.PassengerStatus
                }).ToList()
            };

            return Ok(dto);
        }
    }
}