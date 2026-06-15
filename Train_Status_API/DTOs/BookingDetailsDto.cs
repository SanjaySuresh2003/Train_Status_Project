namespace Train_Status_API.DTOs
{
    public class BookingDetailsDto
    {
        public string Pnr { get; set; } = null!;
        public string TrainNumber { get; set; } = null!;
        public string TrainName { get; set; } = null!;
        public DateOnly JourneyDate { get; set; }

        public string SourceStationName { get; set; } = null!;
        public string DestinationStationName { get; set; } = null!;

        public string ClassCode { get; set; } = null!;
        public string ClassName { get; set; } = null!;

        public string BookingStatus { get; set; } = null!;
        public DateTime BookingDate { get; set; }

        public List<PassengerDto> Passengers { get; set; } = new();
    }

    public class PassengerDto
    {
        public string PassengerName { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string? SeatNumber { get; set; }
        public string? CoachNumber { get; set; }
        public string PassengerStatus { get; set; } = null!;
    }
}