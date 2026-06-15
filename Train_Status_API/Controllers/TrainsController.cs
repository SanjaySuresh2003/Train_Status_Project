using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Status_API.Data;
using Train_Status_API.DTOs;
using Train_Status_API.Models;

namespace Train_Status_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainsController : ControllerBase
    {
        private readonly TrainBookingDbContext _context;

        public TrainsController(TrainBookingDbContext context)
        {
            _context = context;
        }

       
        // FEATURE 1: Search train by train number
        
        [HttpGet("by-number/{trainNumber}")]
        public async Task<ActionResult<TrainSearchDto>> GetByNumber(string trainNumber)
        {
            var train = await _context.Trains
                .Where(t => t.TrainNumber == trainNumber)
                .Select(t => new TrainSearchDto
                {
                    TrainId = t.TrainId,
                    TrainNumber = t.TrainNumber,
                    TrainName = t.TrainName,
                    TrainType = t.TrainType,
                    SourceStationCode = t.SourceStation.StationCode,
                    SourceStationName = t.SourceStation.StationName,
                    DestinationStationCode = t.DestinationStation.StationCode,
                    DestinationStationName = t.DestinationStation.StationName,
                    RunsMon = t.RunsMon,
                    RunsTue = t.RunsTue,
                    RunsWed = t.RunsWed,
                    RunsThu = t.RunsThu,
                    RunsFri = t.RunsFri,
                    RunsSat = t.RunsSat,
                    RunsSun = t.RunsSun
                })
                .FirstOrDefaultAsync();

            if (train == null)
                return NotFound(new { message = $"No train found with number '{trainNumber}'" });

            return Ok(train);
        }

       
        // FEATURE 2: Search train by train name (partial match)
        
        [HttpGet("by-name/{trainName}")]
        public async Task<ActionResult<IEnumerable<TrainSearchDto>>> GetByName(string trainName)
        {
            var trains = await _context.Trains
                .Where(t => t.TrainName.Contains(trainName))
                .Select(t => new TrainSearchDto
                {
                    TrainId = t.TrainId,
                    TrainNumber = t.TrainNumber,
                    TrainName = t.TrainName,
                    TrainType = t.TrainType,
                    SourceStationCode = t.SourceStation.StationCode,
                    SourceStationName = t.SourceStation.StationName,
                    DestinationStationCode = t.DestinationStation.StationCode,
                    DestinationStationName = t.DestinationStation.StationName,
                    RunsMon = t.RunsMon,
                    RunsTue = t.RunsTue,
                    RunsWed = t.RunsWed,
                    RunsThu = t.RunsThu,
                    RunsFri = t.RunsFri,
                    RunsSat = t.RunsSat,
                    RunsSun = t.RunsSun
                })
                .ToListAsync();

            if (trains.Count == 0)
                return NotFound(new { message = $"No trains found matching '{trainName}'" });

            return Ok(trains);
        }

        // ---------------------------------------------------------
        // FEATURE 4: Search trains from one station to another on a date
        // GET /api/trains/search?from=HWH&to=NDLS&date=2026-06-15
        // ---------------------------------------------------------
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RouteSearchResultDto>>> SearchByRoute(
            [FromQuery] string fromCode,
            [FromQuery] string toCode,
            [FromQuery] DateOnly date)
        {
            // Step 1: Find trains whose route includes BOTH the "from" and "to"
            // stations, with "from" appearing BEFORE "to" in the sequence.
            var query =
                from t in _context.Trains
                join fromRoute in _context.TrainRoutes on t.TrainId equals fromRoute.TrainId
                join toRoute in _context.TrainRoutes on t.TrainId equals toRoute.TrainId
                join fromStation in _context.Stations on fromRoute.StationId equals fromStation.StationId
                join toStation in _context.Stations on toRoute.StationId equals toStation.StationId
                where fromStation.StationCode == fromCode
                   && toStation.StationCode == toCode
                   && fromRoute.SequenceNumber < toRoute.SequenceNumber
                select new
                {
                    Train = t,
                    FromRoute = fromRoute,
                    ToRoute = toRoute,
                    FromStation = fromStation,
                    ToStation = toStation
                };

            var matches = await query.ToListAsync();

            // Step 2: Figure out which day of week the requested date falls on,
            // and filter to only trains that run on that day.
            int dayOfWeek = (int)date.DayOfWeek; // Sunday=0, Monday=1, ..., Saturday=6

            var result = matches
                .Where(m => RunsOnDay(m.Train, dayOfWeek))
                .Select(m => new RouteSearchResultDto
                {
                    TrainId = m.Train.TrainId,
                    TrainNumber = m.Train.TrainNumber,
                    TrainName = m.Train.TrainName,
                    TrainType = m.Train.TrainType,
                    FromStationCode = m.FromStation.StationCode,
                    FromStationName = m.FromStation.StationName,
                    DepartureTime = m.FromRoute.DepartureTime,
                    FromDay = m.FromRoute.DayNumber,
                    ToStationCode = m.ToStation.StationCode,
                    ToStationName = m.ToStation.StationName,
                    ArrivalTime = m.ToRoute.ArrivalTime,
                    ToDay = m.ToRoute.DayNumber,
                    DistanceKm = m.ToRoute.DistanceFromSource - m.FromRoute.DistanceFromSource
                })
                .ToList();

            if (result.Count == 0)
                return NotFound(new { message = $"No trains found from '{fromCode}' to '{toCode}' on {date:yyyy-MM-dd}" });

            return Ok(result);
        }

        // Helper: checks if a train runs on a given day of week (0=Sunday...6=Saturday)
        private static bool RunsOnDay(Train train, int dayOfWeek)
        {
            return dayOfWeek switch
            {
                0 => train.RunsSun,
                1 => train.RunsMon,
                2 => train.RunsTue,
                3 => train.RunsWed,
                4 => train.RunsThu,
                5 => train.RunsFri,
                6 => train.RunsSat,
                _ => false
            };
        }
    }
}